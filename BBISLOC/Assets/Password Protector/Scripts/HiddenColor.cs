#region Author
/*
     
     Jones St. Lewis Cropper (caLLow)
     
     Another caLLowCreation
     
     Visit us on Google+ and other social media outlets @caLLowCreation
     
     Thanks for using our product.
     
     Send questions/comments/concerns/requests to 
      e-mail: caLLowCreation@gmail.com
      subject: PasswordProtector
     
*/
#endregion

using System.Collections;
using UnityEngine;

namespace PasswordProtector
{
    /// <summary>
    /// Class to show how to use PasswordProtector
    /// <para>Hides the secret and allows the user to match the password</para>
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class HiddenColor : MonoBehaviour
    {
        [Header("Treasure or Trash")]
        [SerializeField]        
        Color m_SuccessColor = Color.cyan;
        
        [SerializeField]
        Color m_FailedColor = Color.red;

        [PasswordProtect(true)]
        [SerializeField]
        string m_Secret = "24680";

        [Header("Enter the secret to change light.")]
        [PasswordProtect]
        [SerializeField]
        string m_Password = string.Empty;

        /// <summary>
        /// Starts the checker for matched password everytime the script is enabled
        /// </summary>
        void OnEnable()
        {
            StartCoroutine(StartChecking());
        }

        /// <summary>
        /// Checks for the matching password until the script is disabled
        /// </summary>
        /// <returns></returns>
        IEnumerator StartChecking()
        {
            var light = GetComponent<Light>();
            var originalColor = light.color;
            while(enabled)
            {
                if(m_Password.ToString().Equals(m_Secret))
                {
                    if(light.color != m_SuccessColor)
                    {
                        light.color = m_SuccessColor;
                    }
                }
                else if(string.IsNullOrEmpty(m_Password.ToString()))
                {
                    if(light.color != originalColor)
                    {
                        light.color = originalColor;
                    }
                }
                else
                {
                    if(light.color != m_FailedColor)
                    {
                        light.color = m_FailedColor;
                    }
                }
                yield return null;
            }
        }

    }
}