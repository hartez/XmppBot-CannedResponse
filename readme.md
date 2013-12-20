# Canned Responses Plugin for XmppBot

This is a simple plugin for [XmppBot For HipChat](https://github.com/patHyatt/XmppBot-for-HipChat). It loads up a `.json` file of triggers and phrases; when someone in the room says one of the triggers the bot will respond with the matching phrase.

## Installation

Copy the .dlls (Bender.dll, SimpleConfig.dll, XmppBot.Common.dll, and XmppBot-CannedResponse.dll) into the /plugins folder. 
 
## Configuration

Add the following to the `<configSections>` element of XmppBot's configuration file:

    <section name="cannedResponseConfig" type="SimpleConfig.Section, SimpleConfig, Version=1.0.29.0, Culture=neutral, PublicKeyToken=null"/>

Then add the following to the `configuration ` element:

	<cannedResponseConfig ResponseFilePath="plugins/CannedResponses.json">
	</cannedResponseConfig>

Where `ResponseFilePath` is the path to the `.json` file containing the possible phrases.

## File Format

The `.json` file should contain an array of phrase objects; phrase objects need to specify the Trigger, whether the trigger is an ExactMatch, and the response Text. For example: 

	[
	{ExactMatch:"true", Trigger:"This is the exact trigger", Text:"This is the exact response"},
	{ExactMatch:"false", Trigger:"trap", Text:"It's a trap!"}
	]
	
In the example, any string with 'trap' in it will trigger the response; to trigger the first response, the entire string must match the trigger phrase.
	
