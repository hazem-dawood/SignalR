
const logInService = (function () {
    var apiUrls = {
        SignIn: "/Account/SignIn"
    };
    const logIn = function () {
        var
            /**click to sign in*/
            btnSignIn = $("#btnSignIn"),
            /**it must be a text box (input) but you know it's a temp application*/
            ddlUserName = $("#ddlUserName"),
            /**write your password*/
            txtPassword = $("#txtPassword"),
            /**contains all chats with new users chats*/
            divChats = $("#divChats"),
            /**the div that contains the username and password inputs*/
            divSignIn = $("#divSignIn"),
            /**form of logout exists in layout it appears after sign in*/
            frmLogOut = $("#frmLogOut"),
            /**anti forgery token*/
            aToken = $('input[name="AToken"]').val(),
            lblFullName = $("#lblFullName");

        /**after click the signin button*/
        btnSignIn.click(() => {
            btnSignIn.hide();
            $.post(apiUrls.SignIn,
                { 'userName': ddlUserName.val(), 'password': txtPassword.val(), aToken: aToken /*anti forgery token*/ },
                (res) => {
                    if (res.isSuccess) {
                        divSignIn.addClass("d-none");
                        divChats.removeClass("d-none");
                        frmLogOut.removeClass("d-none");
                        frmLogOut.find("label").text(res.data.fullName);
                        initSignalR();
                        chatsService.loadUserChats();
                        lblFullName.text(res.data.name);
                    } else {
                        btnSignIn.show();
                    }
                });
        });

    }

    return {
        /**contains logic for log in */
        logIn: logIn
    }
})();
