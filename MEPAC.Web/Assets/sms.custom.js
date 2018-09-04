/// <reference path="js/jquery-2.1.4.min.js" />


    $(function(){
        var numchk = new RegExp("^[0-9]*$");     /* Using this expression console.log() will display "Numeric value" if the input field value is blank or if it is numeric */  
        $("#input.numbersOnly").keyup(function () {                   /* This function is called whenever input field with id "number" loses focus */
            if (numchk.test($("#input.numbersOnly").val())) {
                console.log("Numeric value");
            }else{
                console.log("Value not numeric");
            }
        });
    });
$("input.numbersOnly").keyup(function () {
    console.log("vô");
    if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
       this.value = this.value.replace(/[^0-9\.]/g, '');
    }
});