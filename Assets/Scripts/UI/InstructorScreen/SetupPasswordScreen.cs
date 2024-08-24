using TMPro;
using UnityEngine;

namespace CodeLearn.InstructorScreen
{
    public class SetupPasswordScreen : MonoBehaviour
    {
        [SerializeField] private TMP_InputField passwordField;
        
        public void Confirm()
        {
            PasswordManager.password = passwordField.text;
        }
    }
}