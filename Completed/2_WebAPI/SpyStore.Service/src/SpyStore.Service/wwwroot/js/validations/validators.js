$.validator.addMethod("greaterthanzero", function (value, element, params) {
    return value > 0;
});

$.validator.unobtrusive.adapters.add("greaterthanzero", function (options) {
    options.rules["greaterthanzero"] = true;
    options.messages["greaterthanzero"] = options.message;
});
$.validator.addMethod("begreaterthan", function (value, element, params) {
    return +value <= +$(params).val();
});
$.validator.unobtrusive.adapters.add("begreaterthan", ["otherpropertyname"], function (options) {
    options.rules["begreaterthan"] = "#item_" + options.params.otherpropertyname;
    options.messages["begreaterthan"] = options.message;
});
