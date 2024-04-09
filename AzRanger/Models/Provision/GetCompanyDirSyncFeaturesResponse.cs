namespace AzRanger.Models.Provision
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://provisioning.microsoftonline.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://provisioning.microsoftonline.com/", IsNullable = false)]
    public partial class GetCompanyDirSyncFeaturesResponse
    {

        private GetCompanyDirSyncFeaturesResponseGetCompanyDirSyncFeaturesResult getCompanyDirSyncFeaturesResultField;

        /// <remarks/>
        public GetCompanyDirSyncFeaturesResponseGetCompanyDirSyncFeaturesResult GetCompanyDirSyncFeaturesResult
        {
            get
            {
                return this.getCompanyDirSyncFeaturesResultField;
            }
            set
            {
                this.getCompanyDirSyncFeaturesResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://provisioning.microsoftonline.com/")]
    public partial class GetCompanyDirSyncFeaturesResponseGetCompanyDirSyncFeaturesResult
    {

        private DirSyncFeatureDetails[] returnValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebServic" +
            "e")]
        [System.Xml.Serialization.XmlArrayItemAttribute("DirSyncFeatureDetails", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
        public DirSyncFeatureDetails[] ReturnValue
        {
            get
            {
                return this.returnValueField;
            }
            set
            {
                this.returnValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class DirSyncFeatureDetails
    {

        private string dirSyncFeatureField;

        private bool enabledField;

        /// <remarks/>
        public string DirSyncFeature
        {
            get
            {
                return this.dirSyncFeatureField;
            }
            set
            {
                this.dirSyncFeatureField = value;
            }
        }

        /// <remarks/>
        public bool Enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }
    }

}
