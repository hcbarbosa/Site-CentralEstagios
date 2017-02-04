<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeradorImagem.aspx.cs" Inherits="CentralEstagios.Views.GeradorImagem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/GerarImagem.css" rel="stylesheet" />

    <script type="text/javascript">
        function acertarHeight() {
            document.getElementById('labelBeneficios').style.height = (document.getElementById('informacaoBeneficios').clientHeight) + "px";

            document.getElementById('labelBeneficios').style.height -= "30px";
            document.getElementById('informacaoBeneficios').style.height -= "30px";
        }
    </script>

    <title></title>
</head>
<body onload="acertarHeight();">
    <form id="form" runat="server">
        <div>
            <div id="logo">
                <center>
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Resources/site_logo.png"></asp:Image>
                </center>
            </div>

            <div id="atributos">

                <div id="divVaga">
                    <div id="labelVaga" class="labelTexto">
                        <p>Vaga:</p>
                    </div>
                    <div id="informacaoVaga" class="labelInformacao">
                        <p>
                            <asp:Label ID="descricao" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divEmpresa">
                    <div id="labelEmpresa" class="labelTexto">
                        <p>Empresa:</p>
                    </div>
                    <div id="informacaoEmpresa" class="labelInformacao">
                        <p>
                            <asp:Label ID="empresa" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divHorario">
                    <div id="labelHorario" class="labelTexto">
                        <p>Horário:</p>
                    </div>
                    <div id="informacaoHorario" class="labelInformacao">
                        <p>
                            <asp:Label ID="horario" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divPeriodo">
                    <div id="labelPeriodo" class="labelTexto">
                        <p>Período:</p>
                    </div>
                    <div id="informacaoPeriodo" class="labelInformacao">
                        <p>
                            <asp:Label ID="periodo" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divTipoVaga">
                    <div id="labelTipoVaga" class="labelTexto">
                        <p>Tipo da Vaga:</p>
                    </div>
                    <div id="informacaoTipoVaga" class="labelInformacao">
                        <p>
                            <asp:Label ID="tipoVaga" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divRemuneracao">
                    <div id="labelRemuneracao" class="labelTexto">
                        <p>Remuneração:</p>
                    </div>
                    <div id="informacaoRemuneracao" class="labelInformacao">
                        <p>
                            <asp:Label ID="remuneracao" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divBeneficios">
                    <div id="labelBeneficios" class="labelTexto">
                        <p>Benefícios:</p>
                    </div>
                    <div id="informacaoBeneficios" class="informacaoBeneficios">
                        <p>
                            <asp:Label ID="beneficios" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divObservacoes">
                    <div id="labelObservacoes" class="labelTexto">
                        <p>Observações:</p>
                    </div>
                    <div id="informacaoObservacoes" class="labelInformacao">
                        <p>
                            <asp:Label ID="observacoes" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divTelefone">
                    <div id="labelTelefone" class="labelTexto">
                        <p>Telefone:</p>
                    </div>
                    <div id="informacaoTelefone" class="labelInformacao">
                        <p>
                            <asp:Label ID="telefone" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divEmail">
                    <div id="labelEmail" class="labelTexto">
                        <p>E-mail:</p>
                    </div>
                    <div id="informacaoEmail" class="labelInformacao">
                        <p>
                            <asp:Label ID="email" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

                <div id="divPessoaContato">
                    <div id="labelPessoaContato" class="labelTexto">
                        <p>Pessoa para contato:</p>
                    </div>
                    <div id="informacaoPessoaContato" class="labelInformacao">
                        <p>
                            <asp:Label ID="pessoaContato" runat="server"></asp:Label>
                        </p>
                        <br />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
