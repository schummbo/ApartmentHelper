# ApartmentHelper

This is a little utility I put together to help us find a new apartment.  It will enumerate all of the Craigslist apartment listings for Seattle and order them according to the folowing criteria, from lowest relevancy to highest:

- Apartments where the program was unable to determine where it actually was. (These tend to look bad or scammy in practice)
- Apartments that take less than or equal to an hour to get to "destination" by bus.
- Apartments that are actually in Seattle
- Apartments that only require one bus to get to "destination"

# Usage
You will need an API key for Google Directions and Google Geocode. Please add these to the app.config. The program will also be expecting a street address for some kind of destination to get to by bus.  For me, this is my office.  Please add this to the app.config as well.

Run the program, and upon completion, you will see an HTML file with apartment listings ordered by the above criteria.
