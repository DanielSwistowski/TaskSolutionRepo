$('#btnSearch').click(function () {
    clearFields();
    getCompanyDetails();
});

function getCompanyDetails() {
    if (!$('#companyNumber').val()) {
        $('#message').html('Wpisz numer');
    } else {
        $('#message').html('Wyszukuję...');
        var number = $('#companyNumber').val();
        var apiUrl = "http://localhost:51567/api/company/details";
        $.getJSON(apiUrl, { number: number })
          .done(function (data) {
              displayCompanyDetails(data);
          })
          .fail(function (jqXHR, textStatus, error) {
              var msg = handleAjaxError(jqXHR, textStatus, error);
              $('#message').html(msg);
          });
    }
}

function isEmpty(selector) {
    return !$.trim(selector.html())
}

function clearFields() {
    $('#nip').html('');
    $('#regon').html('');
    $('#krs').html('');
    $('#companyName').html('');
    $('#city').html('');
    $('#street').html('');
    $('#houseNumber').html('');
    $('#zipCode').html('');
    $('#message').html('');
}

function displayCompanyDetails(details) {
    if (details != null) {
        $('#message').html('Wyszukiwanie zakończone');
        $('#nip').html(details.nip);
        $('#regon').html(details.regon);
        $('#krs').html(details.krs);
        $('#companyName').html(details.companyName);
        $('#city').html(details.city);
        $('#street').html(details.street);
        $('#houseNumber').html(details.houseNumber);
        $('#zipCode').html(details.zipCode);
    }
}

function handleAjaxError(jqXHR, textStatus, error) {
    var errorMessage = 'Nieznany błąd';
    if (jqXHR.status == 500) {
        errorMessage = 'Błąd serwera';
    } else if (jqXHR.status == 404) {
        errorMessage = 'Nie znaleziono firmy';
    } else if (jqXHR.status == 400) {
        errorMessage = 'Niepoprawny numer';
    }
    return errorMessage;
}