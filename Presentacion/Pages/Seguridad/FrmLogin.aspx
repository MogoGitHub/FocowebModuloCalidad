<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmLogin.aspx.vb" Inherits="Presentacion.FrmLoginV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Foco en Obra | Escritorio de Proyectos</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="https://code.highcharts.com"></script>
    <link rel="stylesheet" href="../../CSS/w3.css" />
    <link rel="stylesheet" href="../../CSS/w3-theme-black.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Lato" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>

    <script>
        function muestraLoader() {
            if (myform.fname.value.length > 0 || myform.fpass.value.length > 0) {
                document.getElementById("div_msj").style.visibility = "hidden";
                document.getElementById("loader").style.display = "block";
               
                       
            }
           
            
        }
    </script>

    <script type="text/javascript">
        function validar() {
            if (myform.fname.value.length == 0) {
                document.getElementById('errfn').innerHTML = "Error en su nombre de usuario";

            }

            else if (myform.fpass.value.length == 0) {
                document.getElementById('errfn').innerHTML = "Error en su contraseña.";

            }

            

        }
//        function Ingresar() {
//            if (myform.fname.value.length > 0 && myform.fpass.value.length > 0) {
//                document.getElementById('msg').innerHTML = "Cargando...";
//            }
//        }
    </script>

    <style>
        /* Center the loader */
        #loader {
            position: absolute;
            left: 50%;
            top:75%;
            z-index: 1;
            width: 80px;
            height: 80px;
            margin: -50px 0px 0px -15px;
            border: 16px solid #40b3df;
            border-radius: 100%;
            border-top: 16px solid #17649a;
            width: 30px;
            height: 30px;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 1s linear infinite;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        /* Add animation to "page content" */
        .animate-bottom {
            position: relative;
            -webkit-animation-name: animatebottom;
            -webkit-animation-duration: 1s;
            animation-name: animatebottom;
            animation-duration: 1s
        }

        @-webkit-keyframes animatebottom {
            from {
                bottom: -100px;
                opacity: 0
            }

            to {
                bottom: 0px;
                opacity: 1
            }
        }

        @keyframes animatebottom {
            from {
                bottom: -100px;
                opacity: 0
            }

            to {
                bottom: 0;
                opacity: 1
            }
        }

        #myDiv {
            display: none;
            text-align: center;
        }
    </style>


</head>
<body>
    <%--Main Wrapper--%>
    <div class="w3-main" id="main">
        <div class="w3-container w3-content" style="max-width: 1400px">
            <div id="loader" style="display:none"></div>
            <div class="w3-row-padding w3-container w3-display-middle w3-card-4 w3-theme-l4 w3-border" style="width: 85%; max-width: 320px; margin-top: -45px">
                <div class="w3-row-padding w3-center w3-padding-16">
                    <img src="http://focoenobra.cl/images/logo.png" alt="Foco En Obra" style="width: 75%" />
                </div>
                <div class="w3-row w3-center ">
                    <%--  <div class="w3-row w3-section"><h2>Bienvenido</h2></div>--%>
                    <%--   <div class="w3-row w3-section"><p>Ingrese su nombre de usuario y contraseña</p></div>--%>

                    <div class="w3-row w3-section ">
                        <form id="myform" runat="server">


                            <div class="w3-row w3-section">
                                <div class="w3-col" style="width: 50px"><i class="w3-xxlarge fa fa-user" style="color: #ccc"></i></div>
                                <div class="w3-rest">
                                    <input id="fname" class="w3-input w3-border" runat="server" name="first" type="text" placeholder="Nombre Usuario" autocomplete="off" required>
                                </div>
                            </div>

                            <div class="w3-row w3-section">
                                <div class="w3-col" style="width: 50px"><i class="w3-xxlarge fa fa-lock" style="color: #ccc"></i></div>
                                <div class="w3-rest">
                                    <input id="fpass" class="w3-input w3-border" runat="server" name="last" type="password" placeholder="Contraseña" required>
                                </div>
                            </div>



                            <%--<input id="fname" runat="server" class="w3-input w3-border" name="first" type="text" placeholder="Nombre Usuario" required >
                <div class="w3-row w3-section">   
                <input id="fpass" runat ="server" class="w3-input w3-border" name="last" type="password" placeholder="Contraseña" required >            
                </div>--%>

                            <div class="w3-row w3-section">
                                <asp:Button ID="btnIngresar3" runat="server" class="w3-btn-block w3-section w3-blue w3-ripple w3-padding" OnClick="Ingresar_Click" OnClientClick="muestraLoader()" Text="Ingresar" />
                            </div>

                            <%--                <div class="w3-row w3-section">

 <input class="w3-check" type="checkbox">
                    </div>--%>
                        </form>
                        <%--<label class="w3-validate">¿Recordar esta cuenta?</label>--%>
                    </div>

                </div>

                <div class="w3-container w3-center w3-theme-l4 w3-small" style="margin-top: -40px">
                    <p>
                        Foco En Obra<br />
                        Gestión Por Compromisos
                    </p>

                </div>
                <div id="div_msj" runat="server" class="w3-row w3-center w3-round-xlarge w3-border w3-margin-bottom " style="background-color: #fafafa; visibility: hidden">

                    <div class="w3-row w3-section" style="height: 50px;">
                        <div class="w3-col w3-right-align" style="width: 20%; height: 100%"><i id="icoMsj" runat="server" class="fa fa-exclamation-triangle fa-2x" style="color: #17649a"></i></div>
                        <div class="w3-rest">
                            <p class="w3-center" id="msg" runat="server" style="margin-top: 0px; color: #1a1a1a"></p>
                            
                        </div>
                    </div>

                    
                </div>
            </div>
        </div>

    </div>

</body>
</html>

