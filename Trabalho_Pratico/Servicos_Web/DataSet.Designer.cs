﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace Servicos_Web {
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("DataSet")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class DataSet : global::System.Data.DataSet {
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataSet() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected DataSet(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override global::System.Data.DataSet Clone() {
            DataSet cln = ((DataSet)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars(bool initTable) {
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitClass() {
            this.DataSetName = "DataSet";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/DataSet.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            DataSet ds = new DataSet();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
    }
}
namespace Servicos_Web.DataSetTableAdapters {
    
    
    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class QueriesTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.IDbCommand[] _commandCollection;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected global::System.Data.IDbCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.IDbCommand[1];
            this._commandCollection[0] = new global::System.Data.SqlClient.SqlCommand();
            ((global::System.Data.SqlClient.SqlCommand)(this._commandCollection[0])).Connection = new global::System.Data.SqlClient.SqlConnection(global::Servicos_Web.Properties.Settings.Default.Trabalho_ISI_ESConnectionString);
            ((global::System.Data.SqlClient.SqlCommand)(this._commandCollection[0])).CommandText = "SELECT        [Noticias_Nao Residente].[Não ResidenteValidacão], [Noticias_Nao Re" +
                "sidente].NoticiasIDNoticias, [Noticias_Nao Residente].[Nao ResidenteID NaoReside" +
                "nte], Administrador.[FuncionarioID Funcionario], \r\n                         Func" +
                "ionario.[ID Funcionario], Funcionario.[PessoaID Pessoa], Funcionario.Funçao, Not" +
                "icias.IDNoticias, Noticias.Descriçao, Noticias.TipoNoticia, Noticias.[Funcionari" +
                "oID Funcionario] AS Expr1, \r\n                         [Nao Residente].[PessoaID " +
                "Pessoa] AS Expr2, [Nao Residente].Validacão, [Nao Residente].[ID NaoResidente], " +
                "Noticias_Residente.[ResidenteID Residente], Noticias_Residente.ResidenteValidacã" +
                "o, \r\n                         Noticias_Residente.NoticiasIDNoticias AS Expr3, Pe" +
                "ssoa.[ID Pessoa], Pessoa.Nome, Pessoa.Password, Pessoa.Contribuinte, Pessoa.[Num" +
                "ero Cartão Cidadão], Pessoa.[Data Nascimento], Pessoa.[MoradaID Morada], \r\n     " +
                "                    Morada.[ID Morada], Morada.Rua, Morada.N_Porta, Morada.CP_ID" +
                ", CP.CP_ID AS Expr4, CP.Localidade, Residente.[ID Residente], Residente.Validacã" +
                "o AS Expr5, Residente.[PessoaID Pessoa] AS Expr6, Servicos.[ID Serviço], \r\n     " +
                "                    Servicos.Descriçao_Serviço, Servicos.[ResidenteID Residente]" +
                " AS Expr7, Servicos.[FuncionarioID Funcionario] AS Expr8, Servicos.ResidenteVali" +
                "dacão AS Expr9, Servicos.Data, Servicos.DocsIDDoc, Docs.IDDoc, \r\n               " +
                "          Docs.Descriçao AS Expr10, [Servicos Gerais].[ID Serviço Geral], [Servi" +
                "cos Gerais].Descriçao_Serviço AS Expr11, [Servicos Gerais].[FuncionarioID Funcio" +
                "nario] AS Expr12, [Servicos Gerais].[ResidenteID Residente] AS Expr13, \r\n       " +
                "                  [Servicos Gerais].ResidenteValidacão AS Expr14, [Servicos Gera" +
                "is].Data AS Expr15, [Servicos Gerais].[Nao ResidenteValidacão], [Servicos Gerais" +
                "].[Nao ResidenteID NaoResidente] AS Expr16\r\nFROM            [Noticias_Nao Reside" +
                "nte] INNER JOIN\r\n                         Administrador INNER JOIN\r\n            " +
                "             Funcionario ON Administrador.[FuncionarioID Funcionario] = Funciona" +
                "rio.[ID Funcionario] INNER JOIN\r\n                         Noticias ON Funcionari" +
                "o.[ID Funcionario] = Noticias.[FuncionarioID Funcionario] ON [Noticias_Nao Resid" +
                "ente].NoticiasIDNoticias = Noticias.IDNoticias INNER JOIN\r\n                     " +
                "    [Nao Residente] ON [Noticias_Nao Residente].[Não ResidenteValidacão] = [Nao " +
                "Residente].Validacão AND [Noticias_Nao Residente].[Nao ResidenteID NaoResidente]" +
                " = [Nao Residente].[ID NaoResidente] INNER JOIN\r\n                         Notici" +
                "as_Residente ON Noticias.IDNoticias = Noticias_Residente.NoticiasIDNoticias INNE" +
                "R JOIN\r\n                         Pessoa ON Funcionario.[PessoaID Pessoa] = Pesso" +
                "a.[ID Pessoa] AND [Nao Residente].[PessoaID Pessoa] = Pessoa.[ID Pessoa] INNER J" +
                "OIN\r\n                         Morada INNER JOIN\r\n                         CP ON " +
                "Morada.CP_ID = CP.CP_ID ON Pessoa.[MoradaID Morada] = Morada.[ID Morada] INNER J" +
                "OIN\r\n                         Residente ON Noticias_Residente.[ResidenteID Resid" +
                "ente] = Residente.[ID Residente] AND Noticias_Residente.ResidenteValidacão = Res" +
                "idente.Validacão AND Pessoa.[ID Pessoa] = Residente.[PessoaID Pessoa] INNER JOIN" +
                "\r\n                         Servicos ON Funcionario.[ID Funcionario] = Servicos.[" +
                "FuncionarioID Funcionario] AND Residente.[ID Residente] = Servicos.[ResidenteID " +
                "Residente] AND Residente.Validacão = Servicos.ResidenteValidacão INNER JOIN\r\n   " +
                "                      Docs ON Servicos.DocsIDDoc = Docs.IDDoc INNER JOIN\r\n      " +
                "                   [Servicos Gerais] ON Funcionario.[ID Funcionario] = [Servicos" +
                " Gerais].[FuncionarioID Funcionario] AND [Nao Residente].Validacão = [Servicos G" +
                "erais].[Nao ResidenteValidacão] AND \r\n                         [Nao Residente].[" +
                "ID NaoResidente] = [Servicos Gerais].[Nao ResidenteID NaoResidente] AND Resident" +
                "e.[ID Residente] = [Servicos Gerais].[ResidenteID Residente] AND \r\n             " +
                "            Residente.Validacão = [Servicos Gerais].ResidenteValidacão";
            ((global::System.Data.SqlClient.SqlCommand)(this._commandCollection[0])).CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual global::System.Nullable<bool> ScalarQuery() {
            global::System.Data.SqlClient.SqlCommand command = ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[0]));
            global::System.Data.ConnectionState previousConnectionState = command.Connection.State;
            if (((command.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                command.Connection.Open();
            }
            object returnValue;
            try {
                returnValue = command.ExecuteScalar();
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    command.Connection.Close();
                }
            }
            if (((returnValue == null) 
                        || (returnValue.GetType() == typeof(global::System.DBNull)))) {
                return new global::System.Nullable<bool>();
            }
            else {
                return new global::System.Nullable<bool>(((bool)(returnValue)));
            }
        }
    }
}

#pragma warning restore 1591