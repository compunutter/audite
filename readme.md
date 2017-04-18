# Audite

A quick and simple Windows application used in education to deter students from going on proxy websites.

Audite is a Windows Desktop application, it has an empty hidden main form and just hides in the background listening to the user's last few keystrokes. If the user types in the word 'proxy' then Audite will close any instances of IE or Chrome and then show a warning dialogue. The only way to dismiss the dialogue window is by clicking 'I understand'.

'Setup' dir creates MSI installers so that Audite can be deployed across a Windows network using GPOs.

## Screenshot:

![](screenshots/warning.jpg)