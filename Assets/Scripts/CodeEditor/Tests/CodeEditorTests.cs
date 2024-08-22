using System.Collections.Generic;
using CodeEditor.Nodes;
using CodeEditor.Nodes.Literals;
using NUnit.Framework;
using UnityEngine;

namespace CodeLearn.CodeEditor.Tests
{
    [TestFixture]
    public class CodeEditorTests
    {
        [Test]
        public void Variable_Creation_Updates_The_Memory()
        {
            const string numericTestVariable = "NumericTestVariable";
            const string logicTestVariable = "LogicTestVariable";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(numericTestVariable, 20.3f),
                new VariableDeclarationNode(logicTestVariable, true)
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(numericTestVariable);
            bool logicValue = interpreter.MemoryModule.GetValue<bool>(logicTestVariable);
            
            Assert.That(numericValue, Is.EqualTo(20.3f));
            Assert.That(logicValue, Is.EqualTo(true));
        }
        
        [Test]
        public void Assignments_Override_Variable_Values()
        {
            const string numericTestVariable = "NumericTestVariable";
            const string logicTestVariable = "LogicTestVariable";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(numericTestVariable, 20.3f),
                new VariableDeclarationNode(logicTestVariable, true),
                new VariableAssignmentNode<float>(numericTestVariable, new NumericLiteralNode(10f)),
                new VariableAssignmentNode<bool>(logicTestVariable, new LogicalLiteralNode(false))
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(numericTestVariable);
            bool logicValue = interpreter.MemoryModule.GetValue<bool>(logicTestVariable);
            
            Assert.That(numericValue, Is.EqualTo(10f));
            Assert.That(logicValue, Is.EqualTo(false));
        }
        
        [Test]
        public void Variable_Value_Can_Be_Read()
        {
            const string firstNumericTestVariable = "NumericTestVariable1";
            const string secondNumericTestVariable = "NumericTestVariable2";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(firstNumericTestVariable, 0),
                new VariableDeclarationNode(secondNumericTestVariable, -23f),
                new VariableAssignmentNode<float>(firstNumericTestVariable, new VariableGetNode<float>(secondNumericTestVariable)),
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(firstNumericTestVariable);
            Assert.That(numericValue, Is.EqualTo(-23f));
        }
        
        [Test]
        public void Numeric_Values_Can_Be_Summed()
        {
            const string numericTestVariable = "NumericTestVariable1";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(numericTestVariable, 2f),
                new VariableAssignmentNode<float>(numericTestVariable, new SumNode(new VariableGetNode<float>(numericTestVariable), new NumericLiteralNode(2f))),
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(numericTestVariable);
            Assert.That(numericValue, Is.EqualTo(4f));
        }
        
        [Test]
        public void Numeric_Values_Can_Be_Multiplied()
        {
            const string numericTestVariable = "NumericTestVariable1";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(numericTestVariable, 8f),
                new VariableAssignmentNode<float>(numericTestVariable, new MultiplyNode(new VariableGetNode<float>(numericTestVariable), new NumericLiteralNode(2f))),
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(numericTestVariable);
            Assert.That(numericValue, Is.EqualTo(16f));
        }
        
        [Test]
        public void Numeric_Values_Can_Be_Subtracted()
        {
            const string numericTestVariable = "NumericTestVariable1";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(numericTestVariable, 2f),
                new VariableAssignmentNode<float>(numericTestVariable, new SubtractionNode(new VariableGetNode<float>(numericTestVariable), new NumericLiteralNode(2f))),
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(numericTestVariable);
            Assert.That(numericValue, Is.EqualTo(0f));
        }
        
        [Test]
        public void Numeric_Values_Can_Be_Divided()
        {
            const string numericTestVariable = "NumericTestVariable1";
            
            List<ICodeNode> code = new()
            {
                new VariableDeclarationNode(numericTestVariable, 2f),
                new VariableAssignmentNode<float>(numericTestVariable, new DivideNode(new VariableGetNode<float>(numericTestVariable), new NumericLiteralNode(2f))),
            };
            
            CodeInterpreter interpreter = new(code);
            interpreter.Run();

            float numericValue = interpreter.MemoryModule.GetValue<float>(numericTestVariable);
            Assert.That(numericValue, Is.EqualTo(1f));
        }
    }
}
