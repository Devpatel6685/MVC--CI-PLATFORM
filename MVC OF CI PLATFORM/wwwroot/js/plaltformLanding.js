$(document).ready(function () {
    getCountry();
    $('#Cities').attr('disabled', true);
    $('#Countries').change(function () {
        $('#Cities').attr('disabled', false);
        var id = $(this).val();
        $('#Cities').empty();
        $('#Cities').append('<option>City</option>');
        $.ajax({
            url: '/Mission/city?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    console.log(data);
                    $('#Cities').append('<option value=' + data.id + '>' + data.name + '</option>')
                })
            }
        })
    })
});

function getCountry() {
    $.ajax({
        url: '/Mission/country',
        success: function (result) {
            $.each(result, function (i, data) {
                console.log(data);
                $('#Countries').append('<option value=' + data.countryId + '>' + data.name + '</option>')
            })
        }
    })
}







function showList(e) {
    var $gridCont = $('.grid-container');
    e.preventDefault();
    $gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
}

function gridList(e) {
    var $gridCont = $('.grid-container')
        // e.preventDefault();
    $gridCont.removeClass('list-view');
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);



var checkboxes = document.querySelectorAll(".checkbox");


let filtersSection = document.querySelector(".filters-section");

var listArray = [];

var filterList = document.querySelector(".filter-list");

var len = listArray.length;

for (var checkbox of checkboxes) {
    checkbox.addEventListener("click", function() {
        if (this.checked == true) {
            addElement(this, this.value);
        } else {

            removeElement(this.value);
            console.log("unchecked");
        }
    })
}


function addElement(current, value) {
    let filtersSection = document.querySelector(".filters-section");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('ps-3');
    createdTag.classList.add('pe-1');
    createdTag.classList.add('me-2');
    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    let crossButton = document.createElement('button');
    crossButton.classList.add("filter-close-button");
    let cross = '&times;'



    crossButton.addEventListener('click', function() {
        let elementToBeRemoved = document.getElementById(value);

        console.log(elementToBeRemoved);
        console.log(current);
        elementToBeRemoved.remove();

        current.checked = false;




    })

    crossButton.innerHTML = cross;


    // let crossButton = '&times;'

    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);

}

function removeElement(value) {

    let filtersSection = document.querySelector(".filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);

}


function showList(e) {
    var $gridCont = $('.grid-container');
    var $cardstart = $('.card-start');
    var $grid_element = $('.grid-element');
    e.preventDefault();
    $gridCont.addClass('list-view');
    //$gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
    $cardstart.removeClass('d-none');
    $cardstart.addClass('d-flex');
    $grid_element.removeClass('d-flex');
    $grid_element.addClass('d-none');
}
function gridList(e) {
    var $gridCont = $('.grid-container')
    var $cardstart = $('.card-start');
    var $grid_element = $('.grid-element');
    // e.preventDefault();
    $gridCont.removeClass('list-view');
    $cardstart.addClass('d-flex');
    $cardstart.addClass('d-none');
    $grid_element.removeClass('d-none');
    $grid_element.addClass('d-flex');
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);
    
        $(document).ready(function() {
            $('#search-input').on('input', function () {
                console.log('ok');
                var searchQuery = $(this).val().toLowerCase();
                console.log(searchQuery);
                $('.item').each(function () {
                    var cardText = $(this).text().toLowerCase();
                    console.log(cardText);
                    if (cardText.indexOf(searchQuery) !== -1 || searchQuery.length === 0) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });

                var totalcard = document.getElementsByClassName('item').length;
                var disablecard = document.getElementsByClassName('item d-none').length
                var enablecard = totalcard - disablecard;
                document.getElementById('count').innerHTML = enablecard;
                console.log(enablecard)
                if (enablecard == 0) {
                    $('.container').empty();
                    $('.container').append('<div class="d-flex mt-5 flex-column"><div class="mx-auto"><h3>No Mission Found</h3></div><div class="mx-auto mt-3"><button class="py-2 px-3 mx-auto apply">Submit New Mission <img src="Images\right-arrow.png" alt="" class="ms-2"></button></div> </div>')
                }

            });
        });

/*var totalcard = document.getElementsByClassName('item').length;
var disablecard = document.getElementsByClassName('item d-none').length
var enablecard = totalcard - disablecard;
document.getElementById('count').innerHTML = enablecard;
console.log(enablecard)
if (enablecard == 0) {
    $('.container').empty();
    $('.container').append('<div class="d-flex mt-5 flex-column"><div class="mx-auto"><h3>No Mission Found</h3></div><div class="mx-auto mt-3"><button class="py-2 px-3 mx-auto apply">Submit New Mission <img src="Images\right-arrow.png" alt="" class="ms-2"></button></div> </div>')
}*/
  