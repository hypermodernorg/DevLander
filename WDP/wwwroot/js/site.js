function CheckAnswer(id) {

    // Get all the elements with the class: letters
    var lettersGroup = document.getElementsByClassName("letters");
    var jSon = {};

    // construct json object of letter(key) number(value) pairs from all elements with the class: letters


    var data = {
        id: id,
        one: lettersGroup[0].id + "_" + lettersGroup[0].value,
        two: lettersGroup[1].id + "_" + lettersGroup[1].value,
        three: lettersGroup[2].id + "_" + lettersGroup[2].value,
        four: lettersGroup[3].id + "_" + lettersGroup[3].value,
        five: lettersGroup[4].id + "_" + lettersGroup[4].value,
        sex: lettersGroup[5].id + "_" + lettersGroup[5].value,
        seven: lettersGroup[6].id + "_" + lettersGroup[6].value,
        eight: lettersGroup[7].id + "_" + lettersGroup[7].value,
        nine: lettersGroup[8].id + "_" + lettersGroup[8].value,
        ten: lettersGroup[9].id + "_" + lettersGroup[9].value,
    }





    // Now, lets send the object to the code behind.
    // Attempting fetch:

    fetch(`/puzzles/check/`, {
        method: 'POST',
        body: data,
        headers: {
            'Content-type': 'application/json; charset=UTF-8'
        }
    }).then(function (response) {
        if (response.ok) {
            alert("ok");
            return response.json();
        }
        return Promise.reject(response);
    }).then(function (data) {
        console.log(data);
    }).catch(function (error) {
        console.warn('BLAH BLAH BLAH', error);
    });


    


}

function DeletePallet(palletID) {

    if (typeof palletID == 'undefined') {
        if (document.getElementById('palletId')) {
            palletID = document.getElementById('palletId').value;
        }
    }

    if (typeof palletID != 'undefined') {

        $.ajax({
            type: "POST",
            url: '/ColorPallets/DeletePallet',
            data: {
                PalletID: palletID
            },
            dataType: "text",
            success: function (PalletID) {

                if (document.getElementById('palletId')) {
                    if (document.getElementById('palletId').value == PalletID) {
                        window.location.href = window.location.origin;
                    }
                }

            },
            error: function (e) {
                alert('fail');
            }

        });
    }

}