Imports System.Data.SqlClient
Imports System.IO

Public Class AltaClientes

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim objDic As New Dictionary(Of String, String)()

            For Each ObjCultureInfo As System.Globalization.CultureInfo In System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures)

                Dim objRegionInfo As New System.Globalization.RegionInfo(ObjCultureInfo.Name)

                If Not objDic.ContainsKey(objRegionInfo.DisplayName) Then

                    objDic.Add(objRegionInfo.DisplayName, objRegionInfo.TwoLetterISORegionName.ToLower())

                End If

            Next

            Dim obj = objDic.OrderBy(Function(p) p.Key)
            For Each val As KeyValuePair(Of String, String) In obj

                cbPais.Items.Add(val.Key)
                cbPaisDom.Items.Add(val.Key)

            Next

            cbSexo.Items.Add("Hombre", "Hombre")
            cbSexo.Items.Add("Mujer", "Mujer")
        End If
    End Sub

    Protected Sub limpiarCampos() Handles btnLimpiar.Click
        lblId.Text = "--"
        txtPrimNom.Text = ""
        txtSegNom.Text = ""
        txtPrimApe.Text = ""
        txtSegApe.Text = ""
        cbTipCli.Text = ""
        txtRFC.Text = ""
        txtCurp.Text = ""
        txtCalle.Text = ""
        txtNumExt.Text = ""
        txtNumInt.Text = ""
        txtColonia.Text = ""
        txtCP.Value = ""
        txtEmail.Text = ""
        deFecNac.Value = ""
        cbLocalidad.Value = ""
        cbPais.Value = ""
        cbEstado.Value = ""
        cbMunicipio.Value = ""
        txtRef.Text = ""
        txtTel.Value = ""
        txtCel.Value = ""
        cbNacionalidad.Text = ""
        txtEstCivil.Text = ""
        txtOcupacion.Text = ""
        checkAval.Checked = False
        txtPrimNomCon.Text = ""
        txtApellPatCon.Text = ""
        txtApellMatCon.Text = ""
        deFecNacCon.Value = ""
        cbSexo.Text = ""
        txtPuesto.Text = ""
        cbEntidadNac.Value = ""
        cbPaisDom.Value = ""
        txtAntDom.Value = ""
        txtNomEmp.Text = ""
        txtGiroEmp.Text = ""
        txtSector.Text = ""
        txtPuesto.Text = ""
        txtCalleEmp.Text = ""
        txtNumExtEmp.Text = ""
        txtNumIntEmp.Text = ""
        txtColoniaEmp.Text = ""
        txtCPEmp.Text = ""
        cbMunicipioEmp.Value = ""
        cbLocalidadEmp.Value = ""
        cbEstadoEmp.Value = ""
        txtNomNeg.Text = ""
        seAntNeg.Value = ""
        txtTelNeg.Text = ""
        txtGiroNeg.Text = ""
        txtCalleNeg.Text = ""
        txtNumExtNeg.Text = ""
        txtNumIntNeg.Text = ""
        txtColoniaNeg.Text = ""
        txtCPNeg.Text = ""
        cbMunicipioNeg.Value = ""
        cbLocalidadNeg.Value = ""
        cbEstadoNeg.Value = ""
        seEmpleo.Value = 0
        seNegocio.Value = 0
        seConyuge.Value = 0
        seApoyos.Value = 0
        seIngrOtros.Value = 0
        seRenta.Value = 0
        seServicios.Value = 0
        seFamiliares.Value = 0
        seCredito.Value = 0
        seEgrOtros.Value = 0
        txtDepEcoParent1.Text = ""
        seDepEcoEdad1.Value = ""
        txtDepEcoOcup1.Text = ""
        txtDepEcoParent2.Text = ""
        seDepEcoEdad2.Value = ""
        txtDepEcoOcup2.Value = ""
        txtDepEcoParent3.Text = ""
        seDepEcoEdad3.Value = ""
        txtDepEcoOcup3.Text = ""
        txtNomRef1.Text = ""
        txtPrimApeRef1.Text = ""
        txtSegApeRef1.Text = ""
        txtTelRef1.Text = ""
        txtTipRef1.Text = ""
        txtDirRef1.Text = ""
        txtNomRef2.Text = ""
        txtPrimApeRef2.Text = ""
        txtSegApeRef2.Text = ""
        txtTelRef2.Text = ""
        txtTipRef2.Text = ""
        txtDirRef2.Text = ""
        txtNomRef3.Text = ""
        txtPrimApeRef3.Text = ""
        txtSegApeRef3.Text = ""
        txtTelRef3.Text = ""
        txtTipRef3.Text = ""
        txtDirRef3.Text = ""
        txtNomRef4.Text = ""
        txtPrimApeRef4.Text = ""
        txtSegApeRef4.Text = ""
        txtTelRef4.Text = ""
        txtTipRef4.Text = ""
        txtDirRef4.Text = ""

        gvClientes.Selection.UnselectAll()
    End Sub

    Protected Sub txtPrimNom_TextChanged(sender As Object, e As EventArgs) Handles txtPrimNom.TextChanged

        If Not validarCadena(sender.text) Then
            Utilerias.MensajeAlerta("Porfavor solo use letras", Me, True)
            sender.Focus()
        End If

    End Sub

    Private Function validarCadena(cadena As String) As Boolean
        If Not Regex.Match(cadena, "^[a-z]\s[a-z]*$", RegexOptions.IgnoreCase).Success Then
            Return False
        Else Return True

        End If

    End Function



    Protected Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        If Not Regex.Match(sender.Text, "^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$", RegexOptions.IgnoreCase).Success Then
            Utilerias.MensajeAlerta("Introduzca un correo valido", Me, True)
            sender.Focus()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim camposVacios As String = ""

        If txtPrimNom.Text.Trim = "" Then camposVacios = camposVacios & "Primer Nombre, "
        'If txtSegNom.Text.Trim = "" Then camposVacios = camposVacios & "Segundo Nombre, "
        If txtPrimApe.Text.Trim = "" Then camposVacios = camposVacios & "Primer Apellido, "
        'If txtSegApe.Text.Trim = "" Then camposVacios = camposVacios & "Segundo Apellido, "
        If cbTipCli.Text.Trim = "" Then camposVacios = camposVacios & "Tipo Cliente, "
        If txtRFC.Text.Trim = "" Then camposVacios = camposVacios & "RFC, "
        If txtCurp.Text.Trim = "" Then camposVacios = camposVacios & "CURP, "
        If deFecNac.Text.Trim = "" Then camposVacios = camposVacios & "Fecha de Nacimiento, "
        If cbPais.Text.Trim = "" Then camposVacios = camposVacios & "País, "
        If cbMunicipio.Text.Trim = "" Then camposVacios = camposVacios & "Municipio, "
        If cbNacionalidad.Text.Trim = "" Then camposVacios = camposVacios & "Nacionalidad, "
        If cbSexo.Text.Trim = "" Then camposVacios = camposVacios & "Sexo, "
        If txtOcupacion.Text.Trim = "" Then camposVacios = camposVacios & "Ocupación, "
        If txtCel.Text.Trim = "" Then camposVacios = camposVacios & "Celular, "
        If txtEstCivil.Text.Trim = "" Then camposVacios = camposVacios & "Estado Civil, "
        'If txtPrimNomCon.Text.Trim = "" Then camposVacios = camposVacios & "Nombre(s) Conyuge, "
        'If txtApellPatCon.Text.Trim = "" Then camposVacios = camposVacios & "Apellido Paterno  Conyuge, "
        'If txtApellMatCon.Text.Trim = "" Then camposVacios = camposVacios & "Apellido Paterno  Conyuge, "

        If txtCalle.Text.Trim = "" Then camposVacios = camposVacios & "Calle, "
        If txtNumExt.Text.Trim = "" Then camposVacios = camposVacios & "Número Exterior, "
        'If txtNumInt.Text.Trim = "" Then camposVacios = camposVacios & "Número Interior, "
        If txtColonia.Text.Trim = "" Then camposVacios = camposVacios & "Colonia, "
        If txtCP.Text.Trim = "" Then camposVacios = camposVacios & "Código Postal, "
        'If txtEmail.Text.Trim = "" Then camposVacios = camposVacios & "Email, "

        If cbLocalidad.Text.Trim = "" Then camposVacios = camposVacios & "Localidad, "

        If cbEstado.Text.Trim = "" Then camposVacios = camposVacios & "Estado, "

        If txtRef.Text.Trim = "" Then camposVacios = camposVacios & "Referencias, "
        'If txtTel.Text.Trim = "" Then camposVacios = camposVacios & "Teléfono, "




        'If checkAval.Checked = False And cbAval.Text.Trim = "" Then camposVacios = camposVacios & "Aval, "


        If camposVacios <> "" Then
            camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
            If camposVacios.Contains(",") Then
                Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
            Else
                Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
            End If
            Return
        End If

        Dim empresaGuardada = False
        Dim negocioGuardado = False
        Dim infoEcoGuardada = False
        Dim refPerGuardada = False

        Try

            Dim IngresoMensual = seEmpleo.Value + seNegocio.Value + seConyuge.Value + seApoyos.Value + seIngrOtros.Value
            Dim cliente As New clsCliente() With {
            .Id = If(lblId.Text <> "--", lblId.Text, 0),
            .PrimNombre = txtPrimNom.Text,
            .SegNombre = txtSegNom.Text,
            .PrimApellido = txtPrimApe.Text,
            .SegApellido = txtSegApe.Text,
            .Tipo = cbTipCli.Value,
            .RFC = txtRFC.Text,
            .CURP = txtCurp.Text,
            .Email = txtEmail.Text,
            .FecNac = deFecNac.Value,
            .Pais = cbPais.Text,
            .Localidad = cbEntidadNac.Text,
            .Nacionalidad = cbNacionalidad.Text,
            .Sexo = cbSexo.Text,
            .PuestoLaboral = txtPuesto.Text,
            .Ocupacion = txtOcupacion.Text,
            .Celular = txtCel.Text,
            .EstadoCivil = txtEstCivil.Text,
            .NombresConyuge = txtPrimNomCon.Text,
            .PrimApellidoConyuge = txtApellPatCon.Text,
            .SegApellidoConyuge = txtApellMatCon.Text,
            .FecNacConyuge = deFecNacCon.Date,
            .EsAval = checkAval.Checked,
            .IngresoMensual = IngresoMensual,
            .Calle = txtCalle.Text,
            .NumExt = txtNumExt.Text,
            .NumInt = txtNumInt.Text,
            .Colonia = txtColonia.Text,
            .CodPostal = txtCP.Text,
            .LocalidadDomicilio = cbLocalidad.Text,
            .PaisDomicilio = cbPaisDom.Value,
            .EstadoDomicilio = cbEstado.Value,
            .MunicipioDomicilio = cbMunicipio.Value,
            .AntigEnDomicilio = txtAntDom.Value,
            .TelefonoDomicilio = txtTel.Text,
            .TipoContratoLaboral = txtTipoCont.Text,
            .AntigEmpleo = seAntigEmp.Value,
            .AntigEmpAnterior = seAntigEmpAnt.Value,
            .ReferenciaDomicilio = txtRef.Text,
            .Login = Session("Usuario").Login
        }

            Dim empresa As New clsEmpresa() With {
            .Id_Cliente = If(lblId.Text <> "--", lblId.Text, 0),
            .Nombre = txtNomEmp.Text,
            .Giro = txtGiroEmp.Text,
            .Sector = txtSector.Text,
            .Telefono = txtTelEmp.Text,
            .Calle = txtCalleEmp.Text,
            .NumExt = txtNumExtEmp.Text,
            .NumInt = txtNumIntEmp.Text,
            .Colonia = txtColoniaEmp.Text,
            .CodigoPostal = txtCPEmp.Text,
            .Municipio = cbMunicipioEmp.Text,
            .Ciudad = cbLocalidadEmp.Text,
            .Estado = cbEstadoEmp.Text
        }

            Dim negocio As New clsNegocio() With {
            .Id_Cliente = If(lblId.Text <> "--", lblId.Text, 0),
            .Nombre = txtNomNeg.Text,
            .Antiguedad = seAntNeg.Text,
            .Telefono = txtTelNeg.Text,
            .Giro = txtGiroNeg.Text,
            .Calle = txtCalleNeg.Text,
            .NumExt = txtNumExtNeg.Text,
            .NumInt = txtNumIntNeg.Text,
            .Colonia = txtColoniaNeg.Text,
            .CodigoPostal = txtCPNeg.Text,
            .Municipio = cbMunicipioNeg.Text,
            .Ciudad = cbLocalidadNeg.Text,
            .Estado = cbEstadoNeg.Text
        }

            Dim infoEconomica As New clsInfoEconomica() With {
            .Id_Cliente = If(lblId.Text <> "--", lblId.Text, 0),
            .IngEmpleo = seEmpleo.Text,
            .IngNegocio = seNegocio.Text,
            .IngConyuge = seConyuge.Text,
            .IngApoyos = seApoyos.Text,
            .IngOtros = seIngrOtros.Text,
            .EgrRenta = seRenta.Text,
            .EgrServicios = seServicios.Text,
            .EgrGastosFam = seFamiliares.Text,
            .EgrCreditos = seCredito.Text,
            .EgrOtros = seEgrOtros.Text,
            .DepEconParent1 = txtDepEcoParent1.Text,
            .DepEconEdad1 = seDepEcoEdad1.Text,
            .DepEconOcup1 = txtDepEcoOcup1.Text,
            .DepEconParent2 = txtDepEcoParent2.Text,
            .DepEconEdad2 = seDepEcoEdad2.Text,
            .DepEconOcup2 = txtDepEcoOcup2.Text,
            .DepEconParent3 = txtDepEcoParent3.Text,
            .DepEconEdad3 = seDepEcoEdad3.Text,
            .DepEconOcup3 = txtDepEcoOcup3.Text
        }

            Dim refPersonales As New clsRefPersonales() With {
            .Id_Cliente = If(lblId.Text <> "--", lblId.Text, 0),
            .Nombres1 = txtNomRef1.Text,
            .ApellidoPaterno1 = txtPrimApeRef1.Text,
            .ApellidoMaterno1 = txtSegApeRef1.Text,
            .Telefono1 = txtTelRef1.Text,
            .TipoRef1 = txtTipRef1.Text,
            .Direccion1 = txtDirRef1.Text,
            .Nombres2 = txtNomRef2.Text,
            .ApellidoPaterno2 = txtPrimApeRef2.Text,
            .ApellidoMaterno2 = txtSegApeRef2.Text,
            .Telefono2 = txtTelRef2.Text,
            .TipoRef2 = txtTipRef2.Text,
            .Direccion2 = txtDirRef2.Text,
            .Nombres3 = txtNomRef3.Text,
            .ApellidoPaterno3 = txtPrimApeRef3.Text,
            .ApellidoMaterno3 = txtSegApeRef3.Text,
            .Telefono3 = txtTelRef3.Text,
            .TipoRef3 = txtTipRef3.Text,
            .Direccion3 = txtDirRef3.Text,
            .Nombres4 = txtNomRef4.Text,
            .ApellidoPaterno4 = txtPrimApeRef4.Text,
            .ApellidoMaterno4 = txtSegApeRef4.Text,
            .Telefono4 = txtTelRef4.Text,
            .TipoRef4 = txtTipRef4.Text,
            .Direccion4 = txtDirRef4.Text
        }

            If IsNothing(Session("Identificacion")) = False Then cliente.Identificacion = Session("Identificacion") : Session.Remove("Identificacion")
            If IsNothing(Session("Comprobante")) = False Then cliente.ComprobanteDom = Session("Comprobante") : Session.Remove("Comprobante")
            If IsNothing(Session("SolBuro")) = False Then cliente.SolBuro = Session("SolBuro") : Session.Remove("SolBuro")

            If cliente.curpRepetido() Then
                Utilerias.MensajeAlerta("Ya hay un usuario registrado con ese CURP!", Me, True)
                Return
            End If

            If cliente.Id <> 0 Then

                If clsCliente.actualizar(cliente) Then
                    clsCliente.guardarDocumentos(cliente)
                    clsEmpresa.actualizar(empresa)
                    clsNegocio.actualizar(negocio)
                    clsInfoEconomica.actualizar(infoEconomica)
                    clsRefPersonales.actualizar(refPersonales)
                    gvClientes.DataBind()
                    limpiarCampos()
                    Utilerias.MensajeConfirmacion("Se ha actualizado el cliente exitosamente!", Me, True)
                    gvClientes.Selection.UnselectAll()
                Else
                    Utilerias.MensajeAlerta("No se ha podido actualizar el cliente!", Me, True)
                End If
            Else
                If clsCliente.insertar(cliente) Then
                    clsCliente.guardarDocumentos(cliente)

                    Dim id_Cliente = Utilerias.getDataTable("SELECT Id FROM CLIENTES WHERE PrimNombre='" & cliente.PrimNombre & "' AND SegNombre='" & cliente.SegNombre & "' AND PrimApellido='" & cliente.PrimApellido & "' AND SegApellido='" & cliente.SegApellido & "' AND CURP='" & cliente.CURP & "' ").Rows().Item(0).Item(0)

                    empresa.Id_Cliente = id_Cliente
                    empresaGuardada = clsEmpresa.insertar(empresa)
                    negocio.Id_Cliente = id_Cliente
                    negocioGuardado = clsNegocio.insertar(negocio)
                    infoEconomica.Id_Cliente = id_Cliente
                    infoEcoGuardada = clsInfoEconomica.insertar(infoEconomica)
                    refPersonales.Id_Cliente = id_Cliente
                    refPerGuardada = clsRefPersonales.insertar(refPersonales)

                    If (empresaGuardada Or negocioGuardado Or infoEcoGuardada Or refPerGuardada) = False Then
                        Utilerias.MensajeAlerta("No se ha podido guardar la información de: " & If(Not empresaGuardada, "Empresa,", "") & If(Not negocioGuardado, "Negocio,", "") & If(Not infoEcoGuardada, "Información Económica,", "") & If(Not refPerGuardada, "ReferenciasPersonales", ""), Me, True)
                    End If

                    gvClientes.DataBind()
                    limpiarCampos()
                    Utilerias.MensajeConfirmacion("Se ha guardado el cliente exitosamente!", Me, True)
                    gvClientes.Selection.UnselectAll()
                Else
                    Utilerias.MensajeAlerta("No se ha podido guardar el cliente!", Me, True)
                End If
            End If

        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Parece que ha ocurrido un problema! " & ex.Message, Me, True)
        End Try

    End Sub

    Protected Sub gvClientes_SelectionChanged(sender As Object, e As EventArgs) Handles gvClientes.SelectionChanged

        If gvClientes.Selection.Count > 0 Then

            Dim id_cliente = gvClientes.GetSelectedFieldValues("Id").Item(0)
            Dim cliente = clsCliente.obtener(id_cliente)
            Dim empresa = clsEmpresa.obtener(id_cliente)
            Dim negocio = clsNegocio.obtener(id_cliente)
            Dim refPersonales = clsRefPersonales.obtener(id_cliente)
            Dim infoEconomica = clsInfoEconomica.obtener(id_cliente)

            lblId.Text = id_cliente
            txtPrimNom.Text = cliente.PrimNombre
            txtSegNom.Text = cliente.SegNombre
            txtPrimApe.Text = cliente.PrimApellido
            txtSegApe.Text = cliente.SegApellido
            cbTipCli.SelectedItem = cbTipCli.Items.FindByValue(cliente.Tipo.ToString)
            txtRFC.Text = cliente.RFC
            txtCurp.Text = cliente.CURP
            txtEmail.Text = cliente.Email
            deFecNac.Value = cliente.FecNac
            cbPais.Value = cliente.Pais
            cbEntidadNac.Text = cliente.Localidad
            cbNacionalidad.Text = cliente.Nacionalidad
            cbSexo.Text = cliente.Sexo
            txtOcupacion.Text = cliente.Ocupacion
            txtCel.Text = cliente.Celular
            txtEstCivil.Text = cliente.EstadoCivil
            txtPrimNomCon.Text = cliente.NombresConyuge
            txtApellPatCon.Text = cliente.PrimApellidoConyuge
            txtApellMatCon.Text = cliente.SegApellidoConyuge
            deFecNacCon.Value = cliente.FecNacConyuge
            checkAval.Checked = cliente.EsAval

            txtCalle.Text = cliente.Calle
            txtNumExt.Text = cliente.NumExt
            txtNumInt.Text = cliente.NumInt
            txtColonia.Text = cliente.Colonia
            txtCP.Value = cliente.CodPostal
            cbLocalidad.Value = cliente.Localidad
            cbPaisDom.Value = cliente.PaisDomicilio
            cbEstado.Value = cliente.EstadoDomicilio
            cbMunicipio.Value = cliente.MunicipioDomicilio
            txtAntDom.Value = cliente.AntigEnDomicilio
            txtTel.Text = cliente.TelefonoDomicilio
            txtRef.Text = cliente.ReferenciaDomicilio

            txtNomEmp.Text = empresa.Nombre
            txtPuesto.Text = cliente.PuestoLaboral
            txtGiroEmp.Text = empresa.Giro
            txtSector.Text = empresa.Sector
            txtTipoCont.Text = cliente.TipoContratoLaboral
            seAntigEmp.Value = cliente.AntigEmpleo
            seAntigEmpAnt.Value = cliente.AntigEmpAnterior
            txtTelEmp.Value = empresa.Telefono
            txtCalleEmp.Text = empresa.Calle
            txtNumExtEmp.Text = empresa.NumExt
            txtNumIntEmp.Text = empresa.NumInt
            txtColoniaEmp.Text = empresa.Colonia
            txtCPEmp.Text = empresa.CodigoPostal
            cbMunicipioEmp.Text = empresa.Municipio
            cbLocalidadEmp.Text = empresa.Ciudad
            cbEstadoEmp.Text = empresa.Estado

            If Not IsNothing(negocio) Then
                txtNomNeg.Text = negocio.Nombre
                seAntNeg.Value = negocio.Antiguedad
                txtTelNeg.Text = negocio.Telefono
                txtGiroNeg.Text = negocio.Giro
                txtCalleNeg.Text = negocio.Calle
                txtNumExtNeg.Text = negocio.NumExt
                txtNumIntNeg.Text = negocio.NumInt
                txtColoniaNeg.Text = negocio.Colonia
                txtCPNeg.Value = negocio.CodigoPostal
                cbMunicipioNeg.Text = negocio.Municipio
                cbLocalidadNeg.Text = negocio.Ciudad
                cbEstadoNeg.Text = negocio.Estado
            End If




            seEmpleo.Value = infoEconomica.IngEmpleo
            seNegocio.Value = infoEconomica.IngNegocio
            seConyuge.Value = infoEconomica.IngConyuge
            seApoyos.Value = infoEconomica.IngApoyos
            seIngrOtros.Value = infoEconomica.IngOtros
            seRenta.Value = infoEconomica.EgrRenta
            seServicios.Value = infoEconomica.EgrServicios
            seFamiliares.Value = infoEconomica.EgrGastosFam
            seCredito.Value = infoEconomica.EgrCreditos
            seEgrOtros.Value = infoEconomica.EgrOtros
            txtDepEcoParent1.Text = infoEconomica.DepEconParent1
            seDepEcoEdad1.Value = infoEconomica.DepEconEdad1
            txtDepEcoOcup1.Text = infoEconomica.DepEconOcup1
            txtDepEcoParent2.Text = infoEconomica.DepEconParent2
            seDepEcoEdad2.Value = infoEconomica.DepEconEdad2
            txtDepEcoOcup2.Text = infoEconomica.DepEconOcup2
            txtDepEcoParent3.Text = infoEconomica.DepEconParent3
            seDepEcoEdad3.Value = infoEconomica.DepEconEdad3
            txtDepEcoOcup3.Text = infoEconomica.DepEconOcup3
            txtNomRef1.Text = refPersonales.Nombres1
            txtPrimApeRef1.Text = refPersonales.ApellidoPaterno1
            txtSegApeRef1.Text = refPersonales.ApellidoMaterno1
            txtTelRef1.Text = refPersonales.Telefono1
            txtTipRef1.Text = refPersonales.TipoRef1
            txtDirRef1.Text = refPersonales.Direccion1
            txtNomRef2.Text = refPersonales.Nombres2
            txtPrimApeRef2.Text = refPersonales.ApellidoPaterno2
            txtSegApeRef2.Text = refPersonales.ApellidoMaterno2
            txtTelRef2.Text = refPersonales.Telefono2
            txtTipRef2.Text = refPersonales.TipoRef2
            txtDirRef2.Text = refPersonales.Direccion2
            txtNomRef3.Text = refPersonales.Nombres3
            txtPrimApeRef3.Text = refPersonales.ApellidoPaterno3
            txtSegApeRef3.Text = refPersonales.ApellidoMaterno3
            txtTelRef3.Text = refPersonales.Telefono3
            txtTipRef3.Text = refPersonales.TipoRef3
            txtDirRef3.Text = refPersonales.Direccion3
            txtNomRef4.Text = refPersonales.Nombres4
            txtPrimApeRef4.Text = refPersonales.ApellidoPaterno4
            txtSegApeRef4.Text = refPersonales.ApellidoMaterno4
            txtTelRef4.Text = refPersonales.Telefono4
            txtTipRef4.Text = refPersonales.TipoRef4
            txtDirRef4.Text = refPersonales.Direccion4

            'cbAval.SelectedItem = cbTipCli.Items.FindByValue(item(26).ToString)
            'checkAval_CheckedChanged()

            'txtIngreso.Value = item(27)
        End If

    End Sub

    Protected Sub fuIdentificacion_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fuIdentificacion.UploadedComplete
        Dim fs As Stream = fuIdentificacion.PostedFile.InputStream
        Dim br As New BinaryReader(fs)
        Session("Identificacion") = br.ReadBytes(fs.Length)
    End Sub

    Protected Sub fuCompDom_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fuCompDom.UploadedComplete
        Dim fs As Stream = fuCompDom.PostedFile.InputStream
        Dim br As New BinaryReader(fs)
        Session("Comprobante") = br.ReadBytes(fs.Length)
    End Sub

    Protected Sub fuSolBuro_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fuSolBuro.UploadedComplete
        Dim fs As Stream = fuSolBuro.PostedFile.InputStream
        Dim br As New BinaryReader(fs)
        Session("SolBuro") = br.ReadBytes(fs.Length)
    End Sub

    Protected Sub DescargarIdentificacion(sender As Object, e As EventArgs)
        Dim ID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim constr As String = clsConexion.ConnectionString
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = "select Identificacion, ('Identificacion' + PrimNombre + PrimApellido + '.pdf') AS Nombre, 'application/pdf' AS ContentType from clientes where Id=@Id"
                    cmd.Parameters.AddWithValue("@Id", ID)
                    cmd.Connection = con
                    con.Open()

                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        bytes = DirectCast(sdr("Identificacion"), Byte())
                        contentType = sdr("ContentType").ToString()
                        fileName = sdr("Nombre").ToString()
                    End Using
                    con.Close()

                End Using
            End Using
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = contentType
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Este cliente no tiene una Identificación guardada!", Me, True)
        End Try
    End Sub

    Protected Sub DescargarComprobante(sender As Object, e As EventArgs)

        Dim ID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim constr As String = clsConexion.ConnectionString
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = "select ComprobanteDom, ('ComprobanteDomiciliario' + PrimNombre + PrimApellido + '.pdf') AS Nombre, 'application/pdf' AS ContentType from clientes where Id=@Id"
                    cmd.Parameters.AddWithValue("@Id", ID)
                    cmd.Connection = con
                    con.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        bytes = DirectCast(sdr("ComprobanteDom"), Byte())
                        contentType = sdr("ContentType").ToString()
                        fileName = sdr("Nombre").ToString()
                    End Using
                    con.Close()
                End Using
            End Using
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = contentType
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Este cliente no tiene un Comprobante Domiciliario guardado!", Me, True)
        End Try

    End Sub

    Protected Sub DescargarSolBuro(sender As Object, e As EventArgs)

        Dim ID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim constr As String = clsConexion.ConnectionString
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = "select SolBuro, ('SolicitudBuro' + PrimNombre + PrimApellido + '.pdf') AS Nombre, 'application/pdf' AS ContentType from clientes where Id=@Id"
                    cmd.Parameters.AddWithValue("@Id", ID)
                    cmd.Connection = con
                    con.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        bytes = DirectCast(sdr("SolBuro"), Byte())
                        contentType = sdr("ContentType").ToString()
                        fileName = sdr("Nombre").ToString()
                    End Using
                    con.Close()
                End Using
            End Using
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = contentType
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Este cliente no tiene una Solicitud de Buro guardada!", Me, True)
        End Try

    End Sub

    Protected Sub txtSegNom_TextChanged(sender As Object, e As EventArgs) Handles txtSegNom.TextChanged
        If Not validarCadena(sender.text) Then
            sender.Focus()
            Utilerias.MensajeAlerta("Porfavor solo use letras", Me, True)
        End If

    End Sub

    Protected Sub txtPrimApe_TextChanged(sender As Object, e As EventArgs) Handles txtPrimApe.TextChanged
        If Not validarCadena(sender.text) Then
            sender.focus()
            Utilerias.MensajeAlerta("Porfavor solo use letras", Me, True)
        End If


    End Sub

    Protected Sub txtSegApe_TextChanged(sender As Object, e As EventArgs) Handles txtSegApe.TextChanged
        If Not validarCadena(sender.text) Then
            sender.focus()
            Utilerias.MensajeAlerta("Porfavor solo use letras", Me, True)
        End If
    End Sub
End Class
