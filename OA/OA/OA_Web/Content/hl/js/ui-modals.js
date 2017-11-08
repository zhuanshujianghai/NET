var UIModals = function () {


    var initModals = function () {

        $.fn.modalmanager.defaults.resize = true;
        $.fn.modalmanager.defaults.spinner = '<div class="loading-spinner fade" style="width: 200px; margin-left: -100px;"><img src="/Content/hl/image/loading.gif" align="middle">&nbsp;<span style="color: #eee; font-size: 18px; font-family:Open Sans;">&nbsp;Loading...</div>';


        var $modal = $('#ajax-modal');
        var $modal1 = $('#ajax-modal-1');

        $("#sample_editable_1 [rel]").on('click', function () {
            $('body').modalmanager('loading');
            var url = this.rel;
            if (url.split('|')[0] == "container@") {
                setTimeout(function () {
                    $modal1.load(url.split('|')[1], '', function () {
                        $modal1.modal();
                    });
                }, 1000);
            }
            else {
                setTimeout(function () {
                    $modal.load(url, '', function () {
                        $modal.modal();
                    });
                }, 1000);
            }
        });
        $("#sample_editable_3 [rel]").on('click', function () {
            $('body').modalmanager('loading');
            var url = this.rel;
            if (url.split('|')[0] == "container@") {
                setTimeout(function () {
                    $modal1.load(url.split('|')[1], '', function () {
                        $modal1.modal();
                    });
                }, 1000);
            }
            else {
                setTimeout(function () {
                    $modal.load(url, '', function () {
                        $modal.modal();
                    });
                }, 1000);
            }
        });
        $(".clearfix [rel]").on('click', function () {
            $('body').modalmanager('loading');
            var url = this.rel;
            setTimeout(function () {
                $modal.load(url, '', function () {
                    $modal.modal();
                });
            }, 1000);
        });

        $modal.on('click', '.update', function () {
            $modal.modal('loading');
            setTimeout(function () {
                $modal
		      .modal('loading')
		      .find('.modal-body')
		        .prepend('<div class="alert alert-info fade in">' +
		          'Updated!<button type="button" class="close" data-dismiss="alert"></button>' +
		        '</div>');
            }, 1000);
        });

    }

    return {
        //main function to initiate the module
        init: function () {
            initModals();
        }

    };

} ();