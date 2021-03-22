

$(document).ready(() => {
    let id_country;
    let selectedCountry;    
    let clientID;

    $('#addressDetails').css("display", "none");
    $('#endProcess').css("display", "none");

    const Country = () => {
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

    $('#saveClient').click((e) => {

        const objClient = {
            nombre: $('#firstName').val(),
            apellido: $('#lastName').val(),
            telefono: $('#phone').val(),
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
                    url: '/Home/SaveClient',
                    method: 'POST',
                    data: { client: objClient },
                    success: (res) => {
                        const { status } = res;
                        if (status == 200) {
                            const { clientId } = res;

                            clientID = clientId;

                            $('#addressDetails').css("display", "block");
                            $('#userDetails').css("display", "none");

                            $('#endProcess').css("display", "block");
                            $('#saveClient').css("display", "none");
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

    $('#add_Address').click((e) => {

        const objAddress = {
            id_cliente: clientID,
            line1: $('#line1').val(),
            line2: $('#line2').val(),
            sector: $('#county').val(),
            pais: selectedCountry,
            id_pais: id_country,
            ciudad: $('#city').val(),
            zipcode: $('#zipCode').val()
        };

        $.ajax({
            url: '/Home/SaveAdress',
            method: 'POST',
            data: { address: objAddress },
            success: (res) => {                
                const { status } = res;

                if (status == 200) {                    
                    const { direccions } = res;
                    console.log(direccions);
                    
                    let fila = '';
                    $('#addressTable').html("");
                    $.each(direccions, function (key, value) {
                        const { id, line1, line2, sector, pais, ciudad, zipcode } = value;

                        fila += `
                         <tr class="background-secondary mb-3 custom-tr">
                            <th scope="row">${key + 1}</th>
                            <td>${line1}</td>
                            <td>${line2}</td>
                            <td>${sector}</td>
                            <td>${ciudad}</td>       
                            <td>${pais}</td>       
                            <td>${zipcode}</td>    
                            <td class="d-flex">                    
                                <button class="trash-can btn_delete" type="button" data-id=${id}><i class="bi bi-trash"></i> Delete</button>
                            </td>
                         </tr>`;
                    });
                    $('#addressTable').append(fila);

                    clearAddress();
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

       

    });

    $('#endProcess').click(() => {
    
        Swal.fire({
            title: 'Are you sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, do it!'
        }).then((result) => {
            if (result.isConfirmed) {

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'End Process',
                    showConfirmButton: false,
                    timer: 1500
                });

                setTimeout(() => {
                    document.location.href = "/Home";
                },1500)
                
            }
        });

    });

    $(document).on("click", ".btn_delete", function (evt) {
        evt.preventDefault();
        const idAddress = $(this).data("id");

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
                    url: '/Home/DeleteAddressRegistry',
                    method: 'POST',
                    data: { idAddress: idAddress, clientID: clientID },
                    success: (res) => {
                        
                        const { status } = res;
                        if (status == 200) {

                            const { direccions } = res;

                            let fila = '';
                            $('#addressTable').html("");
                            $.each(direccions, function (key, value) {
                                const { id, line1, line2, sector, pais, ciudad, zipcode } = value;

                                fila += `
                                 <tr class="background-secondary mb-3 custom-tr">
                                    <th scope="row">${key + 1}</th>
                                    <td>${line1}</td>
                                    <td>${line2}</td>
                                    <td>${sector}</td>
                                    <td>${ciudad}</td>       
                                    <td>${pais}</td>       
                                    <td>${zipcode}</td>    
                                    <td class="d-flex">                    
                                        <button class="trash-can btn_delete" type="button"><i class="bi bi-trash" data-id=${id}></i> Delete</button>
                                    </td>
                                 </tr>`;
                                });
                            $('#addressTable').append(fila);

                        }
                        if (status == 400) {
                            const { message } = res;
                            console.log(message);
                        }

                    },
                    error: (err) => {
                        console.error(err.responseText);
                    }
                });
            }

        });


    });

    const clearAddress = () => {
        $('#line1').val('');
        $('#line2').val('');
        $('#county').val('');
        $('#city').val('');
        $('#zipCode').val('');
        $('#countrys').prop('selectedIndex', 0);
    };


});