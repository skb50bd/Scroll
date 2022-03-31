document.addEventListener(
    'DOMContentLoaded',
    _ => {
        const tooltipTriggerList =
            [].slice.call(
                document.querySelectorAll(
                    '[data-bs-toggle="tooltip"]'))

        const tooltipList =
            tooltipTriggerList.map(tooltipTriggerEl =>
                new bootstrap.Tooltip(tooltipTriggerEl));

        const jcImagesTooltipInstance =
            JcImagesTooltip.create(
                '.hover-image',
                {
                    width: 128,
                    height: 128
                });

        document
            .querySelectorAll('.js-delete-action')
            .forEach(elem => {
                elem.addEventListener(
                    'click',
                    event => {
                        const deleteActionUrl =
                            event.target.getAttribute('data-delete-url');

                        const redirectUrl =
                            event.target.getAttribute('data-redirect-url');

                        const isDeletionConfirmed =
                            confirm("Are you sure want to delete this item?");

                        if (isDeletionConfirmed) {
                            fetch(deleteActionUrl, {method: 'DELETE'})
                                .then(_ => window.location = redirectUrl);
                        }
                    })
            });
    });