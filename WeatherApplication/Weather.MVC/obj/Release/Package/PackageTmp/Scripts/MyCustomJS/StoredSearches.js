$(document).ready(function () {
    // hide the storage divs if browser doesn't support local storage 
    if (Modernizr.localstorage) {
        showSearch();
    } else {
        if (!Modernizr.localstorage) {
            $("#detectionSpan").html("Your browser doesn't support local storage");
        }
    }

    function showSearch() {
        var id = $('.cityList').val();

        if (id != null) {
            var cityName = $('#city').val();
            var root = location.protocol + '//' + location.host;

            var search = '<a href="' + root + '/Location/' + id + '">' + city + '</a>';
            AddSearch(search);
        }


        // Hämtar ut innehållet i local storage variabeln favorites
        var storedData = localStorage.getItem("searches");
        var storedSearch = JSON.parse(storedData);

        // Skriver ut listan om det finns något innehåll. Gömmer den.
        if (storedSearch != null) {

            $('#tabsection').after('<p class="ptab">Tidigare sökningar</p>');
            $('.form-group').after('<div class="oldSearchesBox"><h4>Dina senaste sökningar<h4></div>');
            jQuery(".oldSearchesBox").hide();

            var root = location.protocol + '//' + location.host;
            $('.oldSearchesBox').append('<ul class="listOfSearchesUL"></ul>');
            for (var index in storedSearch) {
                if (typeof (storedSearch[(storedSearch.length - 1) - index]) != "undefined") { 
                    $(".listOfSearchesUL").append('<li>' + storedSearch[(storedSearch.length - 1) - index] + '</li>');
                }
            }

        }
    }



    function addSearch(search) {

        // Hämtar ut innehållet i local storage variabeln 
        var storedData = localStorage.getItem("searches");
        var storedSearch = JSON.parse(storedData);

        // Om det finns något i variabeln...
        if (storedSearch != null) {

            // Om sökningen redan finns i arrayen tas den gamla bort
            if ($.inArray(search, storedSearch) !== -1) {
                storedSearch.splice($.inArray(search, storedSearch), 1);
            }
            // Tar bort den äldsta sökningen om det finns fler än 10 element
            if (storedSearch.length > 10) {
                storedSearch.shift();
            }

            // Lägger till sökningen i strängen
            storedSearch.push(search);
            localStorage['searches'] = JSON.stringify(storedSearch);
        }
            // ...annars skapas en helt ny array som den nya matrisen läggs till i.
        else {
            var newArray = [];
            newArray.push(search);
            localStorage['searches'] = JSON.stringify(newArray);
        }
    }
});

