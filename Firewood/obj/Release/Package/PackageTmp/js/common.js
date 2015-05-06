$(document).ready(function () {
    //�ж��Ƿ��¼
    var username = getCookie("UserLog").split("+")[0];
    if (username.length > 0) {
        $("#before-log").hide();
        $("#after-log").show();
        $("#uname").text(username);
    }
    //��cookie��ȡ�ǳ�
    function getCookie(c_name) {
        if (document.cookie.length > 0) {
            c_start = document.cookie.indexOf(c_name + "=")
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1
                c_end = document.cookie.indexOf(";", c_start)
                if (c_end == -1) c_end = document.cookie.length
                return unescape(document.cookie.substring(c_start, c_end))
            }
        }
        return "";
    }

    //��ʾ�˵���
    $(".user-btn,#nav-on").mouseover(function (event) {
        $("#nav-on").show();
    }).mouseout(function (event) {
        $("#nav-on").hide();
    });

    //��¼
    $("#user-log-btn").click(function () {
        var sendData = {
            usernum: $.trim($("#usernum").val()),
            password: $.trim($("#password").val())
        };
        $.ajaxJsonPost({
            url: "/Firewood/User/IsLogin",
            contentType: "text/javascript;charset=UTF-8",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function (data) {
                    alert(data.responseJSON.message);
                },
                401: function (data) {
                    alert(data.responseJSON.message);
                }
            },
            success: function () {
                location.href = "/Firewood";
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    //ע��
    $("#user-reg-btn").click(function () {
        var sendData = {
            usernum: $.trim($("#usernum").val()),
            webpwd: $.trim($("#webpwd").val()),
            username: $.trim($("#username").val()),
            usermail: $.trim($("#usermail").val()),
            usertel: $.trim($("#usertel").val()),
            password1: $.trim($("#password1").val()),
            password2: $.trim($("#password2").val())
        };

        $.ajaxJsonPost({
            url: "/Firewood/User/IsRegister",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function (data) {
                    alert(data.responseJSON.message);
                },
                400: function (data) {
                    alert(data.responseJSON.message);
                },
                402: function (data) {
                    alert(data.responseJSON.message);
                },
                500: function (data) {
                    alert(data.responseJSON.message);
                }
            },
            success: function () {
                location.href = "/Firewood";
                //window.location.reload();
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    //��������,��֤ѧ��
    $("#user-check-btn").click(function () {
        var sendData = {
            usernum: $.trim($("#usernum").val()),
            webpwd: $.trim($("#webpwd").val())
        };
        $.ajaxJsonPost({
            url: "/Firewood/User/IsCheckNum",
            contentType: "text/javascript;charset=UTF-8",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function (data) {
                    alert(data.responseJSON.message);
                },
                401: function (data) {
                    alert(data.responseJSON.message);
                },
                402: function (data) {
                    alert(data.responseJSON.message);
                    location.href = "/Firewood/User/Register";
                }
            },
            success: function () {
                location.href = "/Firewood/User/ResetPwd";
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    //��������
    $("#user-reset-btn").click(function () {
        var sendData = {
            password1: $.trim($("#password1").val()),
            password2: $.trim($("#password2").val())
        };
        $.ajaxJsonPost({
            url: "/Firewood/User/IsResetPwd",
            contentType: "text/javascript;charset=UTF-8",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function (data) {
                    alert(data.responseJSON.message);
                },
                401: function (data) {
                    alert(data.responseJSON.message);
                },
                500: function (data) {
                    alert(data.responseJSON.message);
                }
            },
            success: function () {
                location.href = "/Firewood/User/Login";
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    //���Ƹ�����Ϣ
    $("#user-center-btn").click(function () {
        var sendData = {
            usernum: $.trim($("#re-num").val()),
            usermail: $.trim($("#re-mail").val()),
            usertel: $.trim($("#re-tel").val()),
            usergrade: $.trim($("#grade").val()),
            usersex: $("#male").attr("checked") == "checked" ? "0" : "1",
            truename: $.trim($("#truename").val())
        };
        $.ajaxJsonPost({
            url: "/Firewood/User/IsUpdateInfo",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function (data) {
                    alert(data.responseJSON.message);
                },
                400: function (data) {
                    alert(data.responseJSON.message);
                },
                402: function (data) {
                    alert(data.responseJSON.message);
                },
                500: function (data) {
                    alert(data.responseJSON.message);
                }
            },
            success: function () {
                location.href = "/Firewood/User/Center";
            },
            error: function (err) {
                console.log(err);
            }
        });

    });

    //����/��֯��¼
    $("org-log-btn").click(function () {
        var sendData = {
            orgname: $.trim($("#orgname").val()),
            password: $.trim($("#password").val())
        };
        $.ajaxJsonPost({
            url: "/Firewood/Org/IsLogin",
            contentType: "text/javascript;charset=UTF-8",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function (data) {
                    alert(data.responseJSON.message);
                },
                401: function (data) {
                    alert(data.responseJSON.message);
                }
            },
            success: function () {
                location.href = "/Firewood/Org/Index";
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
});