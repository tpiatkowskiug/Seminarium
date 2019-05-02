/// <reference path="jquery-3.3.1.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

jQuery.validator.addMethon('requirediftrue', function (value, element, params) {
    var checkbox = $('#' + params).first();
    var checkboxValue = checkbox.is(":checked");
    return !checkboxValue || value;
}, '');

jQuery.validator.unobtrusive.adapters.addSingleVal("requirediftrue", "boolprop");