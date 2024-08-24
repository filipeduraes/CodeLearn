using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CodeLearn.InstructorScreen
{
    public class PasswordValidator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private UnityEvent onPasswordConfirmed;
        [SerializeField] private UnityEvent onPasswordInvalid;
        
        public void ValidatePassword()
        {
            if (passwordField.text == PasswordManager.password)
            {
                onPasswordConfirmed.Invoke();
            }
            else
            {
                onPasswordInvalid.Invoke();
            }
        }
    }
}