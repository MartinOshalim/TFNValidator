function ValidateTFN() {

    // Disable all the buttons & input
    DisableAllFields();

    UpdateTextToLoading($("#TaxFileValidateText"));
    ActivateSpinner($("#TaxFileValidateSpinner"));

    var data =
    {
        FirstName: $("#FirstNameInput").val(),
        LastName: $("#LastNameInput").val(),
        TaxFileNumber: $('#TaxFileInput').val()
    };

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: "/Person/ValidateTFN",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        complete: function (result) {

            var result = result.responseJSON;

            if (result.isValid === true)
            {
                InputIsValid($('#TaxFileInput'));
                ShowTFNValidationSuccess();
            }
            else
            {
                InputIsInvalid($('#TaxFileInput'));
                ShowTFNValidationError();
            }

            if (result.errorMessage != null && result.errorMessage != "")
            {
                ShowTFNValidationErrorCustomMessage(result.errorMessage);
            }

            //Enable all the buttons and inputs
            EnableAllFields();

            $("#TaxFileValidateText").text("Validate");
            $("#TaxFileValidateSpinner").removeClass("spinner-border");
            $("#TaxFileValidateSpinner").removeClass("spinner-border-sm");
        }
    });
    return false;
}

// disables all buttons and inputs
function DisableAllFields() {
    $(':button').prop('disabled', true);
    $(':input').prop('disabled', true);
}

// Enables all buttons and inputs
function EnableAllFields() {
    $(':button').prop('disabled', false);
    $(':input').prop('disabled', false);
}

//Removes the valid and invalid class.
function RemoveValidCheck(htmlObj) {
    $(htmlObj).removeClass("is-valid");
    $(htmlObj).removeClass("is-invalid");
}

//Adds the valid css class which highlights the input field green and shows the green tick.
function InputIsValid(htmlObj) {
    $(htmlObj).removeClass("is-invalid");
    $(htmlObj).addClass("is-valid");
}

//Adds the valid css class which highlights the input field red and shows the red cross.
function InputIsInvalid(htmlObj) {
    $(htmlObj).removeClass("is-valid");
    $(htmlObj).addClass("is-invalid");
}

//Adds the css class to show the loading spinner.
function ActivateSpinner(htmlObj) {
    $(htmlObj).addClass("spinner-border");
    $(htmlObj).addClass("spinner-border-sm");
}

// Updates the text of the object to loading..
function UpdateTextToLoading(htmlObj) {
    $(htmlObj).text("loading..");
}

// Filters keys typed to ensure only digits are entered. 
// Copied from here - https://www.codeproject.com/Questions/198433/How-to-prevent-entering-letters-in-a-textbox-in-as
// Modified to also accept numpad numbers.
function filterDigits(eventInstance) {
    eventInstance = eventInstance || window.event;
    key = eventInstance.keyCode || eventInstance.which;
    if ((47 < key) && (key < 58) || (key > 95) && (key < 106) || key === 45 || key === 8) {
        return true;
    } else {
        if (eventInstance.preventDefault) eventInstance.preventDefault();
        eventInstance.returnValue = false;
        return false;
    }
}

// Remove the validation success message when you change the TFN number
function RemoveValidationSuccess() {
    var obj = ('#TaxFileNumberValidationMessage');
    $(obj).removeClass("text-success");
    $(obj).addClass("text-danger");
}

// Show a custom validation message
function ShowTFNValidationErrorCustomMessage(message) {
    var obj = ('#TaxFileNumberValidationMessage');
    $(obj).text(message);
    $(obj).addClass("text-danger");    
}

// Show Validation error when repsonse comes back as invalid.
function ShowTFNValidationError() {
    var obj = ('#TaxFileNumberValidationMessage');
    $(obj).text("Please enter a valid Tax File Number.");
}

// Show Validation success when repsonse comes back as valid.
function ShowTFNValidationSuccess() {
    var obj = ('#TaxFileNumberValidationMessage');
    $(obj).text("Tax file number validation successful.")
    $(obj).addClass("text-success");
    $(obj).removeClass("text-danger");
}

// Disable
function DisableValidateButton(inputBox) {
    if ($(inputBox).val().length >= 8) {
        $("#TaxFileValidateButton").removeClass("disabled");
        document.getElementById("TaxFileValidateButton").style.pointerEvents = "";
    }
    else {
        $("#TaxFileValidateButton").addClass("disabled");
        document.getElementById("TaxFileValidateButton").style.pointerEvents = "none";
    }

}