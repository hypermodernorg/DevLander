function CheckAnswer(id) {

    // Get all the elements with the class: letters
    var lettersGroup = document.getElementsByClassName("letters");

    // Now, lets send the object to the code behind.
    // Attempting fetch:


    // Get and return the last letter of the string
    function GetLetter(x) {
         var y = x.slice(-1);

        return y;
    }

    fetch(`/puzzles/check/`, {
        method: 'POST',
        headers: {
            'Content-type': 'application/json',
        },
        body: JSON.stringify({
            id: id,
            one: GetLetter(lettersGroup[0].id) + lettersGroup[0].value,
            two: GetLetter(lettersGroup[1].id)  + lettersGroup[1].value,
            three: GetLetter(lettersGroup[2].id)  + lettersGroup[2].value,
            four: GetLetter(lettersGroup[3].id)  + lettersGroup[3].value,
            five: GetLetter(lettersGroup[4].id)  + lettersGroup[4].value,
            six: GetLetter(lettersGroup[5].id)  + lettersGroup[5].value,
            seven: GetLetter(lettersGroup[6].id)  + lettersGroup[6].value,
            eight: GetLetter(lettersGroup[7].id)  + lettersGroup[7].value,
            nine: GetLetter(lettersGroup[8].id)  + lettersGroup[8].value,
            ten: GetLetter(lettersGroup[9].id)  + lettersGroup[9].value,
        })
    }).then(function (response) {
        if (response.ok) {
            //alert(response);
            return response.json();
        }
        return Promise.reject(response);
    }).then(data => {

        //console.log('Success:', data);

     
            document.getElementById("messageReturned").innerHTML = data;


        function ClearHeading() {
            document.getElementById("messageReturned").innerHTML = "";
        }




        
    }).catch(function (error) {
        console.warn('BLAH BLAH BLAH', error);
    });

    //fetch(`/puzzles/check/${id}`)
    //    .then((response) => { return response.json(); })
    //    .then((data) => { console.log(data); });
    


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