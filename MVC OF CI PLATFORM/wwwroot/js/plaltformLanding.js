$(document).ready(function () {
    getCountry();
    getThemes();
    getSkills();
    $('#city').attr('disabled', true);
    $('#country').change(function () {
        $('#city').attr('disabled', false);
        var link = "/Mission/City?id=";
        /*        var id = $('.country:checkbox:checked');*/
        var ids = []
        $('.country:checkbox:checked').each(function () {
            link = link + ($(this).attr("id")) + '&id=';
        });
        /*        ids = JSON.stringify( 'id'= ids);*/
        console.log(ids);
        $('#city').empty();
        $.ajax({
            url: link,
            data: ids,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#city').append('<div class="form-check ms-3"><input class="form-check-input checkbox" type="checkbox" value=' + data.name + ' id=' + data.id + '><label class="form-check-label" for=' + data.id + '>' + data.name + '</label></div>')
                })
            }
        })
    })

    $('#search-border').change(function () {
        var keyword = $(this).val()
        var link = "/Mission/platformLanding?id=0&pageIndex=1&pageSize=9&SearchInputdata=" + keyword;
        $('body').load(link);
        /*$.ajax({
            url: link,
            success: function () {
                console.log("success");
            }
            })*/
    })
    filter();
});


function getCountry() {
    $.ajax({
        url: '/Mission/Country',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#country').append('<div class="form-check ms-3"><input class="form-check-input checkbox country" type="checkbox" value=' + data.name + ' id=' + data.countryId + '><label class="form-check-label" for=' + data.countryId + '>' + data.name + '</label></div>')
            })
        }
    })
}

function getThemes() {
    $.ajax({
        url: '/Mission/Theme',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#theme').append('<div class="form-check ms-3"><input class="form-check-input checkbox city" type="checkbox" value=' + data.title + ' id=' + data.missionThemeId + '><label class="form-check-label" for=' + data.missionThemeId + '>' + data.title + '</label></div>')
            })
        }
    })
}

function getSkills() {
    $.ajax({
        url: '/Mission/Skill',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#skill').append('<div class="form-check ms-3"><input class="form-check-input checkbox" type="checkbox" value=' + data.skillName + ' id=' + data.skillId + '><label class="form-check-label" for=' + data.ski + '>' + data.skillName + '</label></div>')
            })
        }
    })
}
/*$(document).ready(function () {
    getCountry();
    getThemes();
    getSkills();
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
*//*$(document).ready(function () {
    getCountry();
    getThemes();
    getSkills();
    $('#Cities').attr('disabled', true);
    $('#Countries').change(function () {
        $('#Cities').attr('disabled', false);
        var link = "/Mission/city?id=";
        *//*        var id = $('.country:checkbox:checked');*//*
        var ids = []
        $('.Countries:checkbox:checked').each(function () {
            link = link + ($(this).attr("id")) + '&id=';
        });
        *//*        ids = JSON.stringify( 'id'= ids);*//*
        console.log(ids);
        $('#Cities').empty();
        $.ajax({
            url: link,
            data: ids,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#Cities').append('<div class="form-check ms-3"><input class="form-check-input checkbox" type="checkbox" value=' + data.name + ' id=' + data.id + '><label class="form-check-label" for=' + data.id + '>' + data.name + '</label></div>')
                })
            }
        })
    })*//*
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
    *//*function getCountry() {
        $.ajax({
            url: '/Mission/country',
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#Countries').append('<div class="form-check ms-3"><input class="form-check-input checkbox country" type="checkbox" value=' + data.name + ' id=' + data.countryId + '><label class="form-check-label" for=' + data.countryId + '>' + data.name + '</label></div>')
                })
            }
        })
    }*//*
function getThemes() {
    $.ajax({
        url: '/Mission/theme',
        success: function (result) {
            $.each(result, function (i, data) {
                console.log(data);
                $('#theme').append('<option value=' + data.missionThemeId + '>' + data.title + '</option>')
            })
        }
    })
}
   *//* function getThemes() {
        $.ajax({
            url: '/Mission/theme',
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#theme').append('<div class="form-check ms-3"><input class="form-check-input checkbox" type="checkbox" value=' + data.title + ' id=' + data.missionThemeId + '><label class="form-check-label" for=' + data.missionThemeId + '>' + data.title + '</label></div>')
                })
            }
        })
    }
*//*
function getSkills() {
    $.ajax({
        url: '/Mission/skills',
        success: function (result) {
            $.each(result, function (i, data) {
                console.log(data);
                $('#skill').append('<option value=' + data.skillId + '>' + data.skillName + '</option>')
            })
        }
    })
}

    *//*function getSkills() {
        $.ajax({
            url: '/Mission/skills',
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#skill').append('<div class="form-check ms-3"><input class="form-check-input checkbox" type="checkbox" value=' + data.skillName + ' id=' + data.skillId + '><label class="form-check-label" for=' + data.ski + '>' + data.skillName + '</label></div>')
                })
            }
        })
    }*/



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
                        $(this).removeClass("d-none");
                    } else {
                        $(this).addClass("d-none");
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
  