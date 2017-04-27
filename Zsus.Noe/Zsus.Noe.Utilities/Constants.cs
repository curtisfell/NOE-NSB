using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Utilities
{
    public class Constants
    {
        #region Application Variables
        public const int ZSUS_NOE_UNSET = -1;
        public const int ZSUS_NOE_SUCCESS = 1;
        public const String ZSUS_NOE_SUCCESS_MSG = "Success";
        public const int ZSUS_NOE_FAIL = -1;
        public const int ZSUS_NOE_FALSE = 0;
        public const int ZSUS_NOE_TRUE = 1;
        public const int ZSUS_NOE_STATUS_STARTED = 0;
        public const int ZSUS_NOE_STATUS_STOPPED = 1;
        #endregion

        #region Common Namespaces
        public const string ZSUS_NOE_MESSAGES = "Zsus.Noe.Messages";
        public const string ZSUS_NOE_COMMANDS = "Zsus.Noe.Commands";
        public const string ZSUS_NOE_EVENTS = "Zsus.Noe.Events";
        #endregion

        #region NSB Endpoints
        public const string ZSUS_NOE_SAGA_ENDPOINT = "Zsus.Noe.Saga";
        public const string ZSUS_NOE_SAGA_ENDPOINT_ERROR = "Zsus.Noe.Saga.Error";
        public const string ZSUS_NOE_APPROVE_WCF_ENDPOINT = "Zsus.Noe.Approve";
        public const string ZSUS_NOE_APPROVE_REQUEST_ENDPOINT = "Zsus.Noe.Approve.Request";
        public const string ZSUS_NOE_APPROVE_RESPONSE_ENDPOINT = "Zsus.Noe.Approve.Response";
        public const string ZSUS_NOE_NEGOTIATION_WCF_ENDPOINT = "Zsus.Noe.Negotiation";
        public const string ZSUS_NOE_NEGOTIATION_REQUEST_ENDPOINT = "Zsus.Noe.Negotiation.Request";
        public const string ZSUS_NOE_NEGOTIATION_RESPONSE_ENDPOINT = "Zsus.Noe.Negotiation.Response";
        public const string ZSUS_NOE_OFFER_WCF_ENDPOINT = "Zsus.Noe.Offer";
        public const string ZSUS_NOE_OFFER_REQUEST_ENDPOINT = "Zsus.Noe.Offer.Request";
        public const string ZSUS_NOE_OFFER_RESPONSE_ENDPOINT = "Zsus.Noe.Offer.Response";
        #endregion

        #region DB Connection String
        public const string ZSUS_NOE_DB_ESB_CONNECTION = "mongodb://localhost:27017/NoePocESB";
        public const string ZSUS_NOE_DB_CONNECTION = "mongodb://localhost:27017";
        public const string ZSUS_NOE_DB_DATABASE = "NoePocDB";
        #endregion

        #region WS Endpoints
        public const string ZSUS_NOE_APPROVE_WS_ENDPOINT = "http://localhost:8901/ZsusNoeSubmitAndApproveService";
        #endregion
    }
}
