$(document).ready(() => {

    $('#filterTable').css("display", "none");

    let controladorTiempo = "";

    const filter = () => {
        const filterValue = $("#filterClient").val();

       

        if (filterValue.length > 0) {
            $('#filterTable').css("display", "block");
            $('#homeTable').css("display", "none");

            $.ajax({
                url: '/Home/filterClient',
                method: 'POST',
                data: { filter: filterValue },
                success: (res) => {
                    if (res.length > 0) {
                        let fila = '';
                        $.each(res, (i, value) => {
                            const { id, nombre, apellido, telefono } = value;

                            fila += `
                         <tr class="background-secondary mb-3 custom-tr">
                            <th scope="row">${i + 1}</th>
                            <td>${nombre}</td>
                            <td>${apellido}</td>
                            <td>${telefono}</td>                            
                            <td class="d-flex">                    
                                <a href="/Home/UserDetails/${id}" class="pr-4"><i class="bi bi-box-arrow-in-right"></i> View</a>
                            <button class="trash-can" type="button" data-id="${id}"><i class="bi bi-trash"></i> Delete</button>
                            </td>
                         </tr>`;

                        });
                        $('#tableFilter').html(fila);
                    }
                    else
                    {                        
                        let fila = `
                         <tr class="background-secondary mb-3 custom-tr">
                            <th scope="row">no data</th>
                            <td>no data</td>
                            <td>no data</td>
                            <td>no data</td>                            
                            <td class="d-flex">
                            </td>
                         </tr>`;
                        $('#tableFilter').html(fila);
                    }
               
                },
                error: (err) => {
                    console.error(err.responseText);
                }
            })
        }
        else {
            $('#filterTable').css("display", "none");
            $('#homeTable').css("display", "block");
        }

       
    }

    $("#filterClient").on("keyup", function () {
        clearTimeout(controladorTiempo);
        controladorTiempo = setTimeout(filter, 750);
    });

    $(document).on("click", ".btn_delete", function (evt) {
        evt.preventDefault();
        const idClient = $(this).data("id");

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
                    url: '/Home/DeleteClient',
                    method: 'POST',
                    data: { idClient: idClient },
                    success: (res) => {
                        console.log(res);
                        const { status } = res;
                        if (status == 200) {

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Deleted',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(() => {
                                document.location.href = `/Home`;
                            }, 1500)

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

  

});
