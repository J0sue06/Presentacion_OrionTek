$(document).ready(() => {

    $('#signIn').click((e) => {
        e.preventDefault();
        
        const obj = {
            email: $('#userEmail').val(),
            pass: $('#userPass').val()
        };

        $.ajax({
            url: '/Login/SignIn',
            method: 'POST',
            data: { model: obj },
            success: (res) => {
                const { status, message } = res;

                if (status == 400) {
                    toastr.options = {
                        "closeButton": true,
                        "positionClass": "toast-top-right"
                    }
                    for (var i = 0; i < message.length; i++) {                        
                        toastr["error"](`<h3>${message[i]}</h3>`)
                    }
                }
                if (status == 200) {
                    document.location.href = message;
                }

                console.log(res);
                
            },
            error: (err) => {
                console.error(err.responseText);
            }
        });

    });



});