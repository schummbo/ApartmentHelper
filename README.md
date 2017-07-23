# ApartmentHelper

This is a little utility I put together to help us find a new apartment.  It will enumerate all of the Craigslist apartment listings for Seattle and order them according to the folowing criteria, from lowest relevancy to highest:

- Apartments where the program was unable to determine where it actually was. (These tend to look bad or scammy in practice)
- Apartments that take less than or equal to an hour to get to "destination" by bus.
- Apartments that are actually in Seattle
- Apartments that only require one bus to get to "destination"

# Usage
The program will also be expecting a street address for some kind of destination to get to by bus.  For me, this is my office.  Please add this to the app.config.

Run the program, and upon completion, you will see an HTML file with apartment listings ordered by the above criteria.

# Notes
- This contains a lot of things specific to my situation. Feel free to grab the code and tweak it. For example, we have dogs, so the url for Craigslist listings indicates listings that allow pets. Also, if you live outside of Seattle, you'll have to adjust for that as well.
