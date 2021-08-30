Password Protect Version 1.0.0 10/29/2015

GENERAL USAGE NOTES
==============================================
Add Property Attribute to the string field in any class/script that need to be hidden.
Obfuscate or obscure the text in the input field in the inspector.
Argument for checkbox to make text readable (default to not visible and resets on selection).
Supports undo/redo but when obscured the change may not be seen but does happen.
When obfuscated the inspector value can not be copied or cut tough pasting is still possible.

INSTALLATION
==============================================
Down the package from Unity Asset Store
Import all the assets
In the script with the field to protect, add the [PasswordProtect] attribute to that field
Additionally use [PasswordProtect(true)] to provide a checkbox to show the value in plain text

EXAMPLE
==============================================
The example below will show the field accessKey in the inspector as password characters * in most cases.  The parameter passed as an argument is false by default.  Here set to true shows a checkbox next to the property in the inspector that, when checked, shows the text property in plain text.
----------------------------------------------
[PasswordProtect(true)]
public string accessKey;
----------------------------------------------

CONTACT
==============================================
Developer: caLLowCreation
Author: Jones S. Cropper
E-mail: callowcreation@gmail.com 
	Subject: PasswordProtector
Website: www.caLLowCreation.com