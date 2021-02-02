﻿/*!
  * Licensed under MIT (https://github.com/cdcavell/cdcavell.name/blob/main/LICENSE)
  * 
  *  Revisions:
  *  ----------------------------------------------------------------------------------------------------
  * | Contributor           | Build   | Revison Date | Description 
  * |-----------------------|---------|--------------|-----------------------------------------------------
  * | Christopher D. Cavell | 1.0.0.0 | 10/28/2020   | Initial build 
  * | Christopher D. Cavell | 1.0.0.9 | 11/08/2020   | Implement Registration/Roles/Permissions [#183]
  * | Christopher D. Cavell | 1.0.3.0 | 01/30/2021   | Initial build Authorization Service
  *
  */

console.log('-- Loading site.js (https://github.com/cdcavell/cdcavell.name/blob/main/Source/Web/cdcavell/wwwroot/js/site.js) --');
console.log('Licensed under MIT (https://github.com/cdcavell/cdcavell.name/blob/main/LICENSE)');

$(function() {

    console.info('-- DOM Ready');
    $('#processing').modal('hide');

    $('#processing').on('show.bs.modal', function (e) {
        console.info('-- show processing modal');
    });

    $('#processing').on('shown.bs.modal', function (e) {
        console.info('-- processing modal shown');
    });

    $('#processing').on('hide.bs.modal', function (e) {
        console.info('-- hide processing modal');
    });

    $('#processing').on('hidden.bs.modal', function (e) {
        console.info('-- processing modal hid');
    });

    $('#form').on('submit', function (e) { 
        var data = $("#form :input").serializeArray();
        console.info('-- form submitted');
        console.debug(data); 

        if ($('#form').attr('suppressSubmit') != 'false') {
            e.preventDefault();
            alert('Form submission suppressed while in development');
            console.info('-- form submit suppressed');
        } else {
            $('#processing').modal('show');
        }
    });

});

$(window).on('load', function() {

    console.info('-- Document Ready');

});

function onCaptchaSubmit(captchaToken) {

    $.ajax({
        async: true,
        url: '/Account/ValidateCaptchaToken',
        data: {
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', $('#form')).val(),
            captchaToken: captchaToken
        },
        cache: false,
        type: "POST",
        success: function (data) {

            console.debug('reCAPTCHA: ' + data);
            $('#form').submit();

        },
        error: function (reponse) {

            console.debug('reCAPTCHA error: ' + reponse.responseText);
            alert('Error: Invalid reCAPTCHA response');

        }
    });

};

