$(document).ready(function(){
    $("#job").validate({
        rules: {
            name: {
                required: true,
                minlength: 2
            },
            email: {
                required: true,
                email:true
            },
            mobileno: {
                required: true
            },
            designation: {
                required: true
            },
            resume: {
              required: true
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
            mobileno: {
                required: "Enter Mobile Number"
            },
            designation: {
                required: "Select Designation"
            },
            resume: {
                required: "Select Resume"
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
    var name = $('#name').val();
    var email = $('#email').val();
    var mobileno = $('#mobileno').val();
    var designation = $('#designation').val();
    var resume = $('#resume').prop('files')[0];
    var messages = $('#messages').val();

    var formData = new FormData();

    formData.append('name', name);
    formData.append('email', email);
    formData.append('mobileno', mobileno);
    formData.append('designation', designation);
    formData.append('resume', resume);
    formData.append('messages', messages);

    $.ajax({
        type: "POST",
        url: "sendemailjob.php",
        data: formData,
        contentType: false,
        cache: false,
        processData:false,
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
            $.when($("#sendmessage").html("<p class='textGreen'><strong>Success! Email Sent Successfully</strong></p>").css("display", "inline", "important").fadeOut(4000)).then(function () {
                window.location = "jobapplication.php";
            });
        } else if(responseText.type == 'imagetype'){
            $("#sendmessage").html("<p class='textRed'><strong>Invalid Resume Format</strong></p>").css("display", "inline", "important").fadeOut(5000);
        } else if(responseText.type == 'imagesize'){
            $("#sendmessage").html("<p class='textRed'><strong>Resume size should be less than 1MB</strong></p>").css("display", "inline", "important").fadeOut(5000);
        } else {
            $("#sendmessage").html("<p class='textRed'><strong>Warning! Unable to Sent Email Try again after sometime</strong></p>").css("display", "inline", "important");
        }
    }
}
