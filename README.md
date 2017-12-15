# Cities: Skylines - Custom Namelists
This is a Cities: Skylines addon that provides a framework to add loclized strings, with which new chirps, street names and much more can be added. There are two basic file types:
+ .namelist files provide new localized strings
+ .blacklist files specify which default localized strings (strings within the vanilla locale) to remove before added custom ones

A localized string is simply a string in a certain language, uniquely identified by a combination of
+ identifier, a string representing the type of the string,
+ key, an optional string that acts as a subtype for the identifier,
+ index, a 0-based index that is used to define variants for the same identifier/key combination.

The namelists and blacklists are simple XML files, see the examples for more information on their structure and possible contents.
