/*!
  * Licensed under MIT (https://github.com/cdcavell/cdcavell.name/blob/master/LICENSE)
  * 
  *  Revisions:
  *  ----------------------------------------------------------------------------------------------------
  * | Contributor           | Build | Revison Date | Description 
  * |-----------------------|-------|--------------|-----------------------------------------------------
  * | Christopher D. Cavell | 1.0.0 | 10/26/2020   | Initial build 
  *
  */

console.log('-- Loading site.js (https://github.com/cdcavell/cdcavell.name/blob/master/Source/Web/cdcavell/wwwroot/js/site.js) --');
console.log('Licensed under MIT (https://github.com/cdcavell/cdcavell.name/blob/master/LICENSE)');

$(function() {

    console.log('-- DOM Ready');
    $('#processing').modal('hide');

    $('#processing').on('show.bs.modal', function (e) {
        console.log('-- show processing modal');
    })

    $('#processing').on('shown.bs.modal', function (e) {
        console.log('-- processing modal shown');
    })

    $('#processing').on('hide.bs.modal', function (e) {
        console.log('-- hide processing modal');
    })

    $('#processing').on('hidden.bs.modal', function (e) {
        console.log('-- processing modal hid');
    })

    $('#form').on('submit', function (e) { 
        var data = $("#form :input").serializeArray();
        console.log('-- form submitted');
        console.log(data); 

        if ($('#form').attr('suppressSubmit') != 'false') {
            e.preventDefault();
            alert('Form submission suppressed while in development');
            console.log('-- form submit suppressed');
        } else {
            $('#processing').modal('show');
        }
    });

});

$(window).on('load', function() {

    console.log('-- Document Ready');

});
