/*!
  * Licensed under MIT (https://github.com/cdcavell/cdcavell.name/blob/master/LICENSE)
  */

console.log('-- Loading site.js --');

$(function() {

    $('#loading').on('show.bs.modal', function () {
        console.log('-- show loading modal --');
    })

    $('#loading').on('shown.bs.modal', function () {
        console.log('-- loading modal shown --');
        if ($('#pageLoad').val() === 'true') {
            setTimeout(() => {
                $('#pageLoad').val('false');
                $(this).modal('hide');
            }, 100);
        }
    })

    $('#loading').on('hide.bs.modal', function () {
        console.log('-- hide loading modal --');
    })

    $('#loading').on('hidden.bs.modal', function () {
        console.log('-- loading modal hid --');
    })

    $('#loading').modal('show');

    console.log("-- DOM Loaded --");
});

$(window).on('load', function() {

    console.log("-- Document Loaded --");
});




