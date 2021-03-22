$(document).ready(() => {
    let id_country;
    let selectedCountry;

    const Country = () => {
        id_country = document.getElementById('countrys').value;

        $.ajax({
            url: '/Home/Countrys',
            method: 'GET',
            success: (res) => {
                let _country;
                $.each(res, (i, value) => {
                    const { id, pais1 } = value
                    _country += `<option value=${id}>${pais1}</option>`;
                });
                $('#countrys').append(_country);
            },
            error: (err) => {
                console.error(err.responseText);
            }
        });
    };

    Country();

    $('#countrys').change((e) => {
        id_country = e.target.value;
        selectedCountry = $("#countrys option:selected").text();
    });

    $('#update_Address').click((e) => {

        const objAddress = {
            id: $('#addressID').val(),
            id_cliente: $('#clientID').val(),
            line1: $('#line1').val(),
            line2: $('#line2').val(),
            sector: $('#county').val(),
            pais: selectedCountry,
            id_pais: id_country,
            ciudad: $('#city').val(),
            zipcode: $('#zipCode').val()
        };

        Swal.fire({
            title: 'Are you sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, do it!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: '/Home/SaveChangesAddress',
                    method: 'POST',
                    data: { model: objAddress },
                    success: (res) => {
                        console.log(res);
                        const { status } = res;
                        if (status == 200) {

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Address Update',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(() => {
                                document.location.href = `/Home/UserDetails/${$('#clientID').val()}`;
                            }, 1500)

                        }
                        if (status == 400) {
                            const { message } = res;
                            toastr.options = {
                                "closeButton": true,
                                "positionClass": "toast-top-right"
                            }
                            for (var i = 0; i < message.length; i++) {
                                toastr["error"](`<h3>${message[i]}</h3>`)
                            }
                        }

                    },
                    error: (err) => {
                        console.error(err.responseText);
                    }
                });


            }
        });


    });

});