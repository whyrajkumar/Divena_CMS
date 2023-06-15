$(document).ready(function(){
    $("#contact").validate({
        rules: {
            name: {
                required: true,
                minlength: 2
            },
            email: {
                required: true,
                email:true
            },
            subject: {
                required: true
            },
            message: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            name: {
                required: "Enter First Name",
                minlength: "Enter Minimum 2 Characters in First Name"
            },
            email: {
                required: "Enter Email Id",
                email:"Enter Valid Email Id"
            },
            subject: {
                required: "Enter Subject"
            },
            message: {
                required: "Enter Message",
                minlength: "Enter Minimum 5 Characters in Message"
            }
        },
        errorElement: 'div',
        errorPlacement: function (error, element) {
            var placement = $(element).data('error');
            if (placement) {
                $(placement).append(error)
            } else {
                error.insertAfter(element);
            }
        },
        submitHandler: validationSuccess
    });
});


function validationSuccess() {
    $("button[type=submit]").attr("disabled", "disabled");
    $.ajax({
        type: "POST",
        url: "sendemail.php",
        data: $("#contact").serialize(),
        beforeSend: function () {
            $("#sendmessage").html("<p class='textGreen'><strong>Loading...</strong></p>").css("display", "inline", "important");
        },
        success: formResponse,
        dataType: "json"
    });
}

function formRequest(formData, jqForm, options) {}

function formResponse(responseText, statusText) {
    $("button[type=submit]").removeAttr("disabled", "disabled");
    if (statusText == 'success') {
        if (responseText.type == 'success') {
            $.when($("#sendmessage").html("<p class='textGreen'><strong>Success </strong>Email Sent Successfully</p>").css("display", "inline", "important").fadeOut(4000)).then(function () {
                window.location = "contact.php";
            });
        } else {
            $("#sendmessage").html("<p class='textRed'><strong>Warning </strong> Unable to Sent Email Try again after sometime</p>").css("display", "inline", "important");
        }
    }
}
