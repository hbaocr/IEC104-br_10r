using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC104_dotnet
{
    class IEC104_ASDU_Para
    {

        /***************************************************************************/

        /*protocol para definiton*/
        public enum ApciType
        {
            I_FORMAT,
            S_FORMAT,
            TESTFR_CON,
            TESTFR_ACT,
            STOPDT_CON,
            STOPDT_ACT,
            STARTDT_CON,
            STARTDT_ACT
        }
        public class Description : Attribute
        {
            internal Description(string name)
            {
                Name = name;
            }

            public string Name { get; private set; }

            public static Description GetAttr<T>(T p)
            {
                var info = typeof(T).GetField(System.Enum.GetName(typeof(T), p));
                return (Description)GetCustomAttribute(info, typeof(Description));
            }
        }
        public enum IOA_TypeID
        {
            /**
             * 1 - Single-point information without time tag
             */
            [Description("Single-point information without time tag")]
            M_SP_NA_1 = 1,
            /**
             * 2 - Single-point information with time tag
             */
            [Description("Single-point information with time tag")]
            M_SP_TA_1,
            /**
             * 3 - Double-point information without time tag
             */
            [Description("Double-point information without time tag")]
            M_DP_NA_1,
            /**
             * 4 - Double-point information with time tag
             */
            [Description("Double-point information with time tag")]
            M_DP_TA_1,
            /**
             * 5 - Step position information
             */
            [Description("Step position information")]
            M_ST_NA_1,
            /**
             * 6 - Step position information with time tag
             */
            [Description("Step position information with time tag")]
            M_ST_TA_1,
            /**
             * 7 - Bitstring of 32 bit
             */
            [Description("Bitstring of 32 bit")]
            M_BO_NA_1,
            /**
             * 8 - Bitstring of 32 bit with time tag
             */
            [Description("Bitstring of 32 bit with time tag")]
            M_BO_TA_1,
            /**
             * 9 - Measured value, normalized value
             */
            [Description("Measured value, normalized value")]
            M_ME_NA_1,
            /**
             * 10 - Measured value, normalized value with time tag
             */
            [Description("Measured value, normalized value with time tag")]
            M_ME_TA_1,
            /**
             * 11 - Measured value, scaled value
             */
            [Description("Measured value, scaled value")]
            M_ME_NB_1,
            /**
             * 12 - Measured value, scaled value with time tag
             */
            [Description("Measured value, scaled value with time tag")]
            M_ME_TB_1,
            /**
             * 13 - Measured value, short floating point number
             */
            [Description("Measured value, short floating point number")]
            M_ME_NC_1,
            /**
             * 14 - Measured value, short floating point number with time tag
             */
            [Description("Measured value, short floating point number with time tag")]
            M_ME_TC_1,
            /**
             * 15 - Integrated totals
             */
            [Description("Integrated totals")]
            M_IT_NA_1,
            /**
             * 16 - Integrated totals with time tag
             */
            [Description("Integrated totals with time tag")]
            M_IT_TA_1,
            /**
             * 17 - Event of protection equipment with time tag
             */
            [Description("Event of protection equipment with time tag")]
            M_EP_TA_1,
            /**
             * 18 - Packed start events of protection equipment with time tag
             */
            [Description("Packed start events of protection equipment with time tag")]
            M_EP_TB_1,
            /**
             * 19 - Packed output circuit information of protection equipment with time tag
             */
            [Description("Packed output circuit information of protection equipment with time tag")]
            M_EP_TC_1,
            /**
             * 20 - Packed single-point information with status change detection
             */
            [Description("Packed single-point information with status change detection")]
            M_PS_NA_1,
            /**
             * 21 - Measured value, normalized value without quality descriptor
             */
            [Description("Measured value, normalized value without quality descriptor")]
            M_ME_ND_1,
            /**
             * 30 - Single-point information with time tag CP56Time2a
             */
            [Description("Single-point information with time tag CP56Time2a")]
            M_SP_TB_1 = 30,
            /**
             * 31 - Double-point information with time tag CP56Time2a
             */
            [Description("Double-point information with time tag CP56Time2a")]
            M_DP_TB_1,
            /**
             * 32 - Step position information with time tag CP56Time2a
             */
            [Description("Step position information with time tag CP56Time2a")]
            M_ST_TB_1,
            /**
             * 33 - Bitstring of 32 bits with time tag CP56Time2a
             */
            [Description("Bitstring of 32 bits with time tag CP56Time2a")]
            M_BO_TB_1,
            /**
             * 34 - Measured value, normalized value with time tag CP56Time2a
             */
            [Description("Measured value, normalized value with time tag CP56Time2a")]
            M_ME_TD_1,
            /**
             * 35 - Measured value, scaled value with time tag CP56Time2a
             */
            [Description("Measured value, scaled value with time tag CP56Time2a")]
            M_ME_TE_1,
            /**
             * 36 - Measured value, short floating point number with time tag CP56Time2a
             */
            [Description("Measured value, short floating point number with time tag CP56Time2a")]
            M_ME_TF_1,
            /**
             * 37 - Integrated totals with time tag CP56Time2a
             */
            [Description("Integrated totals with time tag CP56Time2a")]
            M_IT_TB_1,
            /**
             * 38 - Event of protection equipment with time tag CP56Time2a
             */
            [Description("Event of protection equipment with time tag CP56Time2a")]
            M_EP_TD_1,
            /**
             * 39 - Packed start events of protection equipment with time tag CP56Time2a
             */
            [Description("Packed start events of protection equipment with time tag CP56Time2a")]
            M_EP_TE_1,
            /**
             * 40 - Packed output circuit information of protection equipment with time tag CP56Time2a
             */
            [Description("Packed output circuit information of protection equipment with time tag CP56Time2a")]
            M_EP_TF_1,
            /**
             * 45 - Single command
             */
            [Description("Single command")]
            C_SC_NA_1 = 45,
            /**
             * 46 - Double command
             */
            [Description("Double command")]
            C_DC_NA_1,
            /**
             * 47 - Regulating step command
             */
            [Description(" Regulating step command")]
            C_RC_NA_1,
            /**
             * 48 - Set point command, normalized value
             */
            [Description("Set point command, normalized value")]
            C_SE_NA_1,
            /**
             * 49 - Set point command, scaled value
             */
            [Description("Set point command, scaled value")]
            C_SE_NB_1,
            /**
             * 50 - Set point command, short floating point number
             */
            [Description("Set point command, short floating point number")]
            C_SE_NC_1,
            /**
             * 51 - Bitstring of 32 bits
             */
            [Description("Bitstring of 32 bits")]
            C_BO_NA_1,
            /**
             * 58 - Single command with time tag CP56Time2a
             */
            [Description("Single command with time tag CP56Time2a")]
            C_SC_TA_1 = 58,
            /**
             * 59 - Double command with time tag CP56Time2a
             */
            [Description("Double command with time tag CP56Time2a")]
            C_DC_TA_1,
            /**
             * 60 - Regulating step command with time tag CP56Time2a
             */
            [Description("Regulating step command with time tag CP56Time2a")]
            C_RC_TA_1,
            /**
             * 61 - Set-point command with time tag CP56Time2a, normalized value
             */
            [Description("Set-point command with time tag CP56Time2a, normalized value")]
            C_SE_TA_1,
            /**
             * 62 - Set-point command with time tag CP56Time2a, scaled value
             */
            [Description("Set-point command with time tag CP56Time2a, scaled value")]
            C_SE_TB_1,
            /**
             * 63 - C_SE_TC_1 Set-point command with time tag CP56Time2a, short floating point number
             */
            [Description("C_SE_TC_1 Set-point command with time tag CP56Time2a, short floating point number")]
            C_SE_TC_1,
            /**
             * 64 - Bitstring of 32 bit with time tag CP56Time2a
             */
            [Description("Bitstring of 32 bit with time tag CP56Time2a")]
            C_BO_TA_1,
            /**
             * 70 - End of initialization
             */
            [Description("End of initialization")]
            M_EI_NA_1 = 70,
            /**
             * 100 - Interrogation command
             */
            [Description("Interrogation command")]
            C_IC_NA_1 = 100,
            /**
             * 101 - Counter interrogation command
             */
            [Description("Counter interrogation command")]
            C_CI_NA_1,
            /**
             * 102 - Read command
             */
            [Description("Read command")]
            C_RD_NA_1,
            /**
             * 103 - Clock synchronization command
             */
            [Description("Clock synchronization command")]
            C_CS_NA_1,
            /**
             * 104 - Test command
             */
            [Description("Test command")]
            C_TS_NA_1,
            /**
             * 105 - Reset process command
             */
            [Description("Reset process command")]
            C_RP_NA_1,
            /**
             * 106 - Delay acquisition command
             */
            [Description("Delay acquisition command")]
            C_CD_NA_1,
            /**
             * 107 - Test command with time tag CP56Time2a
             */
            [Description("Test command with time tag CP56Time2a")]
            C_TS_TA_1,
            /**
             * 110 - Parameter of measured value, normalized value
             */
            [Description("Parameter of measured value, normalized value")]
            P_ME_NA_1 = 110,
            /**
             * 111 - Parameter of measured value, scaled value
             */
            [Description("Parameter of measured value, scaled value")]
            P_ME_NB_1,
            /**
             * 112 - Parameter of measured value, short floating point number
             */
            [Description("Parameter of measured value, short floating point number")]
            P_ME_NC_1,
            /**
             * 113 - Parameter activation
             */
            [Description("Parameter activation")]
            P_AC_NA_1,
            /**
             * 120 - File ready
             */
            [Description("File ready")]
            F_FR_NA_1 = 120,
            /**
             * 121 - Section ready
             */
            [Description("Section ready")]
            F_SR_NA_1,
            /**
             * 122 - Call directory, select file, call file, call section
             */
            [Description("Call directory, select file, call file, call section")]
            F_SC_NA_1,
            /**
             * 123 - Last section, last segment
             */
            [Description("Last section, last segment")]
            F_LS_NA_1,
            /**
             * 124 - Ack file, ack section
             */
            [Description("Ack file, ack section")]
            F_AF_NA_1,
            /**
             * 125 - Segment
             */
            [Description("Segment")]
            F_SG_NA_1,
            /**
             * 126 - Directory
             */
            [Description("Directory")]
            F_DR_TA_1,
            /**
             * 127 - QueryLog, request archive file
             */
            [Description("QueryLog, request archive file")]
            F_SC_NB_1
        }
        public enum COT_Id
        {
            PERIODIC = 1,
            BACKGROUND_SCAN,
            SPONTANEOUS,
            INITIALIZED,
            REQUEST,
            ACTIVATION,
            ACTIVATION_CON,
            DEACTIVATION,
            DEACTIVATION_CON,
            ACTIVATION_TERMINATION,
            RETURN_INFO_REMOTE,
            RETURN_INFO_LOCAL,
            FILE_TRANSFER,
            INTERROGATED_BY_STATION = 20,
            INTERROGATED_BY_GROUP_1,
            INTERROGATED_BY_GROUP_2,
            INTERROGATED_BY_GROUP_3,
            INTERROGATED_BY_GROUP_4,
            INTERROGATED_BY_GROUP_5,
            INTERROGATED_BY_GROUP_6,
            INTERROGATED_BY_GROUP_7,
            INTERROGATED_BY_GROUP_8,
            INTERROGATED_BY_GROUP_9,
            INTERROGATED_BY_GROUP_10,
            INTERROGATED_BY_GROUP_11,
            INTERROGATED_BY_GROUP_12,
            INTERROGATED_BY_GROUP_13,
            INTERROGATED_BY_GROUP_14,
            INTERROGATED_BY_GROUP_15,
            INTERROGATED_BY_GROUP_16,
            REQUESTED_BY_GENERAL_COUNTER,
            REQUESTED_BY_GROUP_1_COUNTER,
            REQUESTED_BY_GROUP_2_COUNTER,
            REQUESTED_BY_GROUP_3_COUNTER,
            REQUESTED_BY_GROUP_4_COUNTER,
            UNKNOWN_TYPE_ID = 44,
            UNKNOWN_CAUSE_OF_TRANSMISSION,
            UNKNOWN_COMMON_ADDRESS_OF_ASDU,
            UNKNOWN_INFORMATION_OBJECT_ADDRESS
        }
        public class CauseOfTransmission
        {
            /*
             The T or test bit is set when ASDUs are generated for test purposes and are not intended
            to control the process or change the system state. It is used for testing of transmission and equipment.
             */
            public bool test_bit = false;

            /*
             The PN bit is the positive/negative confirmation bit. This is meaningful when used with
            control commands. This bit is used when the control command is mirrored in the monitor
            direction, and it provides indication of whether the command was executed or not. When
            the PN bit is not relevant it is cleared to zero.
             */
            public bool PN = false;
            /*CauseOfTransmission : define */
            public IEC104_ASDU_Para.COT_Id cot_id = 0;

            /*
             The originator address is optional on a system basis. It provides a means for a controlling
            station to explicitly identify itself. This is not necessary when there is only one controlling
            station in a system, but is required when there is more than one controlling station, or
            some stations are dual-mode stations.
             */
            public byte OA = 0;
            IEC104_Setting _config;
            public CauseOfTransmission(IEC104_Setting setting, bool testbit, bool Positive_Negative_confirm, IEC104_ASDU_Para.COT_Id cot, byte OA = 0)
            {
                this.test_bit = testbit;
                this.PN = Positive_Negative_confirm;
                this.cot_id = cot;
                _config = setting;
            }
            //for read COT
            public CauseOfTransmission(IEC104_Setting setting, byte[] revBuff, int pos)
            {
                this._config = setting;

                this.test_bit = ((revBuff[pos] & 0x80) == 0x80);
                this.PN = ((revBuff[pos] & 0x40) == 0x40);
                this.cot_id = (COT_Id)(revBuff[pos] & 0x3F);
                if (_config.cot_size == 2)
                {
                    this.OA = revBuff[pos + 1];
                }
                else
                {
                    this.OA = 0;
                }


            }

            public int byte_encode(byte[] buff, int pos)
            {
                byte t = (test_bit) ? (byte)0x80 : (byte)0;
                byte pn = (PN) ? (byte)0x40 : (byte)0;
                byte cot = (byte)cot_id;
                buff[pos] = (byte)(t | pn | cot);
                if (_config.cot_size == 2)
                {

                    buff[pos + 1] = (byte)OA;
                    return 2;
                }
                else
                {
                    return 1;
                }
            }

        }

        /*Command Information elements*/
        public class SCO_SingleCommandElement
        {
            public enum SE_code { SELECT = 0, EXECUTE = 1 }; //select /excute bit          
            public enum QU_code
            {
                No_Additional_definition = 0,
                Short_pulse_duration = 1,
                Long_pulse_duration = 2,
                Persistent_output = 3
            };//key QOC Quality of cmd
            public enum SCS_code { CMD_OFF = 0, CMD_ON = 1 };//key command (single cmd state)
            public SE_code se;
            public QU_code qu;
            public SCS_code scs;
            public byte SCO_value;
            public SCO_SingleCommandElement(byte value)
            {
                int tmp = 0;
                SCO_value = value;
                this.se = (SE_code)((value & 1 << 7) == 1 << 7 ? SE_code.EXECUTE : SE_code.SELECT);

                tmp = ((value & 0x7C) & 0xFF) >> 2;
                this.qu = (QU_code)tmp;

                this.scs = (value & 0x01) == 1 ? SCS_code.CMD_ON : SCS_code.CMD_OFF;
            }

            public SCO_SingleCommandElement(SE_code se, QU_code qu, SCS_code scs)
            {
                int tmp = 0;

                this.qu = qu;
                this.scs = scs;
                this.se = se;


                byte qu_val = (byte)qu;
                byte scs_val = (byte)scs;

                // tmp = (this.se == SE_code.SELECT ? 1 << 7 : 0) | ((byte)this.qu) << 2 | (byte)this.scs;
                tmp = (se == SE_code.SELECT ? 1 << 7 : 0) | (qu_val << 2) | scs_val;
                SCO_value = (byte)tmp;

            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = SCO_value;
                return 1;
            }
        }
        public class DCO_DoubleCommandElement
        {
            public enum SE_code { SELECT = 0, EXECUTE = 1 }; //select /excute bit          
            public enum QU_code
            {
                No_Additional_definition = 0,
                Short_pulse_duration = 1,
                Long_pulse_duration = 2,
                Persistent_output = 3,
            };//key QOC Quality of cmd
            public enum DCS_code
            {
                CMD_NotPermitted1 = 0,
                CMD_OFF = 1,
                CMD_ON = 2,
                CMD_Notpermitted2 = 3
            };//key command (double cmd state)
            public SE_code se;
            public QU_code qu;
            public DCS_code dcs;
            public byte DCO_value;
            public DCO_DoubleCommandElement(byte value)
            {
                int tmp = 0;
                DCO_value = value;
                this.se = (SE_code)((value & 1 << 7) == 1 << 7 ? SE_code.EXECUTE : SE_code.SELECT);

                tmp = ((value & 0x7C) & 0xFF) >> 2;
                this.qu = (QU_code)tmp;

                this.dcs = (DCS_code)(value & 0x03);
            }

            public DCO_DoubleCommandElement(SE_code se, QU_code qu, DCS_code dcs)
            {
                int tmp = 0;
                this.se = se;
                this.qu = qu;
                this.dcs = dcs;

                tmp = (this.se == SE_code.SELECT ? 1 << 7 : 0) | (byte)this.qu << 2 | (byte)this.dcs;
                DCO_value = (byte)tmp;

            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = DCO_value;
                return 1;
            }
        }
        public class RCO_RegulatingStepCommandElement
        {
            public enum SE_code { SELECT = 0, EXECUTE = 1 }; //select /excute bit          
            public enum QU_code
            {
                No_Additional_definition = 0,
                Short_pulse_duration = 1,
                Long_pulse_duration = 2,
                Persistent_output = 3,
            };//key QOC Quality of cmd
            public enum RCS_code
            {
                CMD_NotPermitted1 = 0,
                CMD_NextStepLower = 1,
                CMD_NextStepHigher = 2,
                CMD_Notpermitted2 = 3
            };//key command 
            public SE_code se;
            public QU_code qu;
            public RCS_code rcs;
            public byte RCO_value;
            public RCO_RegulatingStepCommandElement(byte value)
            {
                int tmp = 0;
                RCO_value = value;
                this.se = (SE_code)((value & 1 << 7) == 1 << 7 ? SE_code.EXECUTE : SE_code.SELECT);

                tmp = ((value & 0x7C) & 0xFF) >> 2;
                this.qu = (QU_code)tmp;

                this.rcs = (RCS_code)(value & 0x03);
            }

            public RCO_RegulatingStepCommandElement(SE_code se, QU_code qu, RCS_code rcs)
            {
                int tmp = 0;
                this.se = se;
                this.qu = qu;
                this.rcs = rcs;

                tmp = (this.se == SE_code.SELECT ? 1 << 7 : 0) | (byte)this.qu << 2 | (byte)this.rcs;
                RCO_value = (byte)tmp;

            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = RCO_value;
                return 1;
            }
        }

        /*Qualifier information Elements*/

        public class QRP_QualifierOfResetProcessElement
        {
            public enum QRP_VALUE
            {
                NOT_USE = 0,
                GENERAL_RESET_OF_PROCESS = 1,
                CLEAR_TIME_TAGGED_INFO_FROM_EVENT_BUFF = 2
            };
            public byte qrp = 0;
            public QRP_QualifierOfResetProcessElement(byte qrp_)
            {
                qrp = qrp_;
            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = qrp;
                return 1;
            }
        }
    
        
        public class QOI_QualifierOfInterrogationElement
        {
            public enum QOI_VALUE
            {
                NOT_USE = 0,
                GLOBAL_STATION_INTERROGATION = 20,
                GROUP1_STATION_INTERROGATION = 21,
                GROUP2_STATION_INTERROGATION = 22,
                GROUP3_STATION_INTERROGATION = 23,
                GROUP4_STATION_INTERROGATION = 24,
                GROUP5_STATION_INTERROGATION = 25,
                GROUP6_STATION_INTERROGATION = 26,
                GROUP7_STATION_INTERROGATION = 27,
                GROUP8_STATION_INTERROGATION = 28,
                GROUP9_STATION_INTERROGATION = 29,
                GROUP10_STATION_INTERROGATION = 30,
                GROUP11_STATION_INTERROGATION = 31,
                GROUP12_STATION_INTERROGATION = 32,
                GROUP13_STATION_INTERROGATION = 33,
                GROUP14_STATION_INTERROGATION = 34,
                GROUP15_STATION_INTERROGATION = 35,
                GROUP16_STATION_INTERROGATION = 36
            };
            public byte qoi = 0;
            public QOI_QualifierOfInterrogationElement(byte qoi_)
            {
                qoi = qoi_;
            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = qoi;
                return 1;
            }
        }

        public class QCC_QualifierOfCounterInterrogationElement
        {
            public enum RQT_code
            {
                NOT_USE = 0,
                GROUP1_COUNTER_INTERROGATION = 1,
                GROUP2_COUNTER_INTERROGATION = 2,
                GROUP3_COUNTER_INTERROGATION = 3,
                GROUP4_COUNTER_INTERROGATION = 4,
                GLOBAL_COUNTER_INTERROGATION = 5
            };
            public enum FRZ_code
            {
                Read_Without_Freeze_Or_Reset = 0,
                Freeze_Without_Reset = 1,
                Freeze_With_Reset = 2,
                Reset = 3
            };
            public FRZ_code frz;
            public RQT_code rqt;
            public byte QCC_val = 0;
            public QCC_QualifierOfCounterInterrogationElement(byte value)
            {
                QCC_val = value;
                frz = (FRZ_code)((value >> 6) & 0x03);
                rqt = (RQT_code)(value & 0x3F);
            }
            public QCC_QualifierOfCounterInterrogationElement(FRZ_code frz, RQT_code rqt)
            {
                this.rqt = rqt;
                this.frz = frz;
                int tmp = ((byte)frz << 6) | ((byte)rqt) & 0xFF;
                QCC_val = (byte)tmp;

            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = QCC_val;
                return 1;
            }
        }
        public class QOC_QualifierOfCommandElement
        {
            public enum SE_code { SELECT = 0, EXECUTE = 1 }; //select /excute bit          
            public enum QU_code
            {
                No_Additional_definition = 0,
                Short_pulse_duration = 1,
                Long_pulse_duration = 2,
                Persistent_output = 3,
            };//key QOC Quality of cmd
            public SE_code se;
            public QU_code qu;
            public byte QOC_val = 0;
            public QOC_QualifierOfCommandElement(byte value)
            {
                QOC_val = value;
                this.se = (SE_code)((value & 1 << 7) == 1 << 7 ? SE_code.EXECUTE : SE_code.SELECT);

                int tmp = ((value & 0x7C) & 0xFF) >> 2;
                this.qu = (QU_code)tmp;
            }
            public QOC_QualifierOfCommandElement(SE_code se, QU_code qu)
            {
                int tmp = 0;
                this.se = se;
                this.qu = qu;
                tmp = (this.se == SE_code.SELECT ? 1 << 7 : 0) | (byte)this.qu << 2;
                QOC_val = (byte)tmp;

            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = QOC_val;
                return 1;
            }
        }
        public class QOS_QualifierOfSetPointCommandElement
        {
            public const byte QL_DEFAULT = 0;
            public enum SE_code { SELECT = 0, EXECUTE = 1 }; //select /excute bit          
            public enum QU_code
            {
                No_Additional_definition = 0,
                Short_pulse_duration = 1,
                Long_pulse_duration = 2,
                Persistent_output = 3,
            };//key QOC Quality of cmd
            public SE_code se;
            public byte ql = 0;
            public byte QOS_val = 0;
            public QOS_QualifierOfSetPointCommandElement(byte value)
            {
                QOS_val = value;
                this.se = (SE_code)((value & 1 << 7) == 1 << 7 ? SE_code.EXECUTE : SE_code.SELECT);
                this.ql = (byte)(value & 0x7F);
            }
            public QOS_QualifierOfSetPointCommandElement(SE_code se, byte ql)
            {
                int tmp = 0;
                this.se = se;
                this.ql = ql;
                tmp = (this.se == SE_code.SELECT ? 1 << 7 : 0) | ql;
                QOS_val = (byte)tmp;

            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = QOS_val;
                return 1;
            }
        }
        public class QDS_QualifierOfDescriptorElement
        {
            public bool iv;
            public bool nt;
            public bool sb;
            public bool bl;
            public bool ov;
            public byte val;
            public QDS_QualifierOfDescriptorElement(byte val)
            {
                this.iv = (val & 0x80) == 0x80;
                this.nt = (val & 0x40) == 0x40;
                this.sb = (val & 0x20) == 0x40;
                this.bl = (val & 0x10) == 0x10;
                this.ov = (val & 0x01) == 0x01;
                this.val = val;
            }
            public QDS_QualifierOfDescriptorElement(bool iv, bool nt, bool sb, bool bl, bool ov)
            {
                int iv_t = iv ? 0x80 : 0;
                int nt_t = nt ? 0x40 : 0;
                int sb_t = sb ? 0x20 : 0;
                int bl_t = bl ? 0x10 : 0;
                int ov_t = ov ? 0x01 : 0;

                this.val = (byte)(iv_t | nt_t | sb_t | bl_t | ov_t);

                this.iv = (this.val & 0x80) == 0x80;
                this.nt = (this.val & 0x40) == 0x40;
                this.sb = (this.val & 0x20) == 0x40;
                this.bl = (this.val & 0x10) == 0x10;
                this.ov = (this.val & 0x01) == 0x01;
            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = val;
                return 1;
            }

        }
        /*Value related to typeID of IOA*/
        public class SIQ_SinglePointInformationElement
        {
            public bool iv;//invalid
            public bool nt;//not topical
            public bool sb;//Substitued
            public bool bl;//blocked
            public bool spi;//status on
            public byte SIQ_value;
            public SIQ_SinglePointInformationElement(byte value)
            {
                iv = (value & 1 << 7) == 1 << 7;
                nt = (value & 1 << 6) == 1 << 6;
                sb = (value & 1 << 5) == 1 << 5;
                bl = (value & 1 << 4) == 1 << 4;
                spi = (value & 1 << 0) == 1 << 0;
                SIQ_value = value;
            }
            public SIQ_SinglePointInformationElement(bool iv, bool nt, bool sb, bool bl, bool spi)
            {
                this.iv = iv;
                this.nt = nt;
                this.sb = sb;
                this.bl = bl;
                this.spi = spi;

                int tm = (this.iv ? 1 << 7 : 0)
                        | (this.nt ? 1 << 6 : 0)
                        | (this.sb ? 1 << 5 : 0)
                        | (this.bl ? 1 << 4 : 0)
                        | (this.spi ? 1 << 1 : 0);
                SIQ_value = (byte)tm;
            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = SIQ_value;
                return 1;
            }
        }
        public class DIQ_DoublePointInformationElement
        {
            public enum DPI_Code
            {
                DPI_Indeterminate = 0,
                DPI_ON = 1,
                DPI_OFF = 2,
                DPI_Indeterminate_state = 3
            }
            public bool iv;//invalid
            public bool nt;//not topical
            public bool sb;//Substitued
            public bool bl;//blocked
            public DPI_Code dpi;//status on
            public byte DIQ_value;
            public DIQ_DoublePointInformationElement(byte value)
            {
                iv = (value & 1 << 7) == 1 << 7;
                nt = (value & 1 << 6) == 1 << 6;
                sb = (value & 1 << 5) == 1 << 5;
                bl = (value & 1 << 4) == 1 << 4;
                dpi = (DPI_Code)(value & 0x03); // last 2 LSB is DCS
                DIQ_value = value;
            }
            public DIQ_DoublePointInformationElement(bool iv, bool nt, bool sb, bool bl, DPI_Code dpi)
            {
                this.iv = iv;
                this.nt = nt;
                this.sb = sb;
                this.bl = bl;
                this.dpi = dpi;
                int tm = (this.iv ? 1 << 7 : 0)
                        | (this.nt ? 1 << 6 : 0)
                        | (this.sb ? 1 << 5 : 0)
                        | (this.bl ? 1 << 4 : 0)
                        | ((int)this.dpi);
                DIQ_value = (byte)tm;
            }
            public int byte_encode(byte[] buff, int pos)
            {
                buff[pos] = DIQ_value;
                return 1;
            }
        }
        public class VTI_ValueWithTransientStateIndicationElement
        {
            public bool transientState;
            public long value;
            public byte vti_val;
            public VTI_ValueWithTransientStateIndicationElement(int value, bool transientState)
            {
                if (value < -64 || value > 63)
                {
                    throw new ArgumentException("Value has to be in the range -64..63");
                }

                this.value = value;
                this.transientState = transientState;
            }

            public VTI_ValueWithTransientStateIndicationElement(byte vti)
            {
                long b1 = vti;
                vti_val = vti;
                transientState = (b1 & 0x80) == 0x80;

                if ((b1 & 0x40) == 0x40)
                {
                    value = (b1 | 0xffffff80);
                }
                else
                {
                    value = b1 & 0x3f;
                }
            }

            public int byte_encode(byte[] buffer, int i)
            {
                if (transientState)
                {
                    buffer[i] = (byte)(value | 0x80);
                }
                else
                {
                    buffer[i] = (byte)(value & 0x7f);
                }

                return 1;
            }

            public long GetValue()
            {
                return value;
            }

            public bool IsTransientState()
            {
                return transientState;
            }

            public override string ToString()
            {
                return "Value with transient state, value: " + GetValue() + ", transient state: " + IsTransientState();
            }
        }
        public class NVA_NormalizedValueInformationElement
        {
            public int IValue;
            public double scaleValue;//in range -1 0 1
            public NVA_NormalizedValueInformationElement(int value)
            {
                if (value < -32768 || value > 32767)
                {
                    throw new ArgumentException("Value has to be in the range -32768..32767");
                }
                IValue = value;
                scaleValue = (double)(IValue / 32768.0000);
            }

            public NVA_NormalizedValueInformationElement(byte[] recBuff, int pos)
            {
                IValue = recBuff[pos] | (recBuff[pos + 1] << 8);
                scaleValue = (double)(IValue / 32768.0000);
            }

            public int byte_encode(byte[] buffer, int i)
            {
                buffer[i++] = (byte)IValue;
                buffer[i] = (byte)(IValue >> 8);

                return 2;
            }

            public int GetValue()
            {
                return IValue;
            }
            public double getNomalizedValue()
            {
                return (double)IValue / 32768.0000;
            }
            public string ToString()
            {
                return "Normalized value: " + (double)IValue / 32768;
            }
        }
        public class SVA_ScaledValueInformationElement
        {
            public int IValue;

            public SVA_ScaledValueInformationElement(int value)
            {
                if (value < -32768 || value > 32767)
                {
                    throw new ArgumentException("Value has to be in the range -32768..32767");
                }
                IValue = value;
            }

            public SVA_ScaledValueInformationElement(byte[] recBuff, int pos)
            {
                IValue = recBuff[pos] | (recBuff[pos + 1] << 8);
            }

            public int byte_encode(byte[] buffer, int i)
            {
                buffer[i++] = (byte)IValue;
                buffer[i] = (byte)(IValue >> 8);

                return 2;
            }

            public int GetValue()
            {
                return IValue;
            }

            public string ToString()
            {
                return "Normalized value: " + (double)IValue / 32768;
            }
        }
        public class R32_ShortFloatingPointNumber
        {
            public float value;

            public R32_ShortFloatingPointNumber(float value)
            {
                this.value = value;
            }

            public R32_ShortFloatingPointNumber(byte[] buff, int pos)
            {
                var data = buff[pos] | (buff[pos + 1] << 8) | (buff[pos + 2] << 16) |
                           (buff[pos + 3] << 24);
                var bytes = BitConverter.GetBytes(data);
                value = BitConverter.ToSingle(bytes, 0);
            }

            public int byte_encode(byte[] buffer, int i)
            {
                var tempVal = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
                buffer[i++] = (byte)tempVal;
                buffer[i++] = (byte)(tempVal >> 8);
                buffer[i++] = (byte)(tempVal >> 16);
                buffer[i] = (byte)(tempVal >> 24);
                return 4;
            }

            public float GetValue()
            {
                return value;
            }

            public string ToString()
            {
                return "Short float value: " + value;
            }
        }

        public class BCR_BinaryCounterReadingElement
        {
            public bool carry;
            public bool counterAdjusted;
            public int counterReading;
            public bool invalid;
            public int sequenceNumber;

            public BCR_BinaryCounterReadingElement(int counterReading, int sequenceNumber, bool carry, bool counterAdjusted,
                bool invalid)
            {
                this.counterReading = counterReading;
                this.sequenceNumber = sequenceNumber;
                this.carry = carry;
                this.counterAdjusted = counterAdjusted;
                this.invalid = invalid;
            }

            public BCR_BinaryCounterReadingElement(byte[] buff, int pos)
            {
                int b1 = buff[pos];
                int b2 = buff[pos + 1];
                int b3 = buff[pos + 2];
                int b4 = buff[pos + 3];
                int b5 = buff[pos + 4];

                carry = (b5 & 0x20) == 0x20;
                counterAdjusted = (b5 & 0x40) == 0x40;
                invalid = (b5 & 0x80) == 0x80;
                sequenceNumber = b5 & 0x1f;
                counterReading = (b4 << 24) | (b3 << 16) | (b2 << 8) | b1;
            }

            public int Encode(byte[] buffer, int i)
            {
                buffer[i++] = (byte)counterReading;
                buffer[i++] = (byte)(counterReading >> 8);
                buffer[i++] = (byte)(counterReading >> 16);
                buffer[i++] = (byte)(counterReading >> 24);

                buffer[i] = (byte)sequenceNumber;
                if (carry)
                {
                    buffer[i] |= 0x20;
                }
                if (counterAdjusted)
                {
                    buffer[i] |= 0x40;
                }
                if (invalid)
                {
                    buffer[i] |= 0x80;
                }

                return 5;
            }

            public int GetCounterReading()
            {
                return counterReading;
            }

            public int GetSequenceNumber()
            {
                return sequenceNumber;
            }

            public bool IsCarry()
            {
                return carry;
            }

            public bool IsCounterAdjusted()
            {
                return counterAdjusted;
            }

            public bool IsInvalid()
            {
                return invalid;
            }

        }


        /*Time information elements*/
        public class CP56Time2aElement
        {

            public byte[] value = new byte[7];

            public CP56Time2aElement(long timestamp, bool invalid)
            {
                var datetime = new DateTime(timestamp);
                var ms = datetime.Millisecond + 1000 * datetime.Second;

                value[0] = (byte)ms;
                value[1] = (byte)(ms >> 8);
                value[2] = (byte)datetime.Minute;

                if (invalid)
                {
                    value[2] |= 0x80;
                }
                value[3] = (byte)datetime.Hour;
                if (datetime.IsDaylightSavingTime())
                {
                    value[3] |= 0x80;
                }
                value[4] = (byte)(datetime.Day + ((((int)datetime.DayOfWeek + 5) % 7 + 1) << 5));
                value[5] = (byte)(datetime.Month + 1);
                value[6] = (byte)(datetime.Year % 100);
            }

            public CP56Time2aElement(long timestamp) : this(timestamp, false) { }

            public CP56Time2aElement(byte[] buff, int pos)
            {
                for (var i = 0; i < 7; i++)
                {
                    this.value[i] = buff[i + pos];
                }
            }

            public int byte_encode(byte[] buff, int pos)
            {
                Array.Copy(value, 0, buff, pos, 7);
                return 7;
            }
            public int GetMillisecond()
            {
                return ((value[0] & 0xff) + ((value[1] & 0xff) << 8)) % 1000;
            }

            public int GetSecond()
            {
                return ((value[0] & 0xff) + ((value[1] & 0xff) << 8)) / 1000;
            }

            public int GetMinute()
            {
                return value[2] & 0x3f;
            }

            public int GetHour()
            {
                return value[3] & 0x1f;
            }

            public int GetDayOfWeek()
            {
                return (value[4] & 0xe0) >> 5;
            }

            public int GetDayOfMonth()
            {
                return value[4] & 0x1f;
            }

            public int GetMonth()
            {
                return value[5] & 0x0f;
            }

            public int GetYear()
            {
                return value[6] & 0x7f;
            }

            public bool IsSummerTime()
            {
                return (value[3] & 0x80) == 0x80;
            }

            public bool IsInvalid()
            {
                return (value[2] & 0x80) == 0x80;
            }

            public string ToString()
            {
                var builder = new StringBuilder("Time56: ");
                AppendWithNumDigits(builder, GetDayOfMonth(), 2);
                builder.Append("-");
                AppendWithNumDigits(builder, GetMonth(), 2);
                builder.Append("-");
                AppendWithNumDigits(builder, GetYear(), 2);
                builder.Append(" ");
                AppendWithNumDigits(builder, GetHour(), 2);
                builder.Append(":");
                AppendWithNumDigits(builder, GetMinute(), 2);
                builder.Append(":");
                AppendWithNumDigits(builder, GetSecond(), 2);
                builder.Append(":");
                AppendWithNumDigits(builder, GetMillisecond(), 3);

                if (IsSummerTime())
                {
                    builder.Append(" DST");
                }

                if (IsInvalid())
                {
                    builder.Append(", invalid");
                }

                return builder.ToString();
            }

            private void AppendWithNumDigits(StringBuilder builder, int value, int numDigits)
            {
                var i = numDigits - 1;
                while (i < numDigits && value < Math.Pow(10, i))
                {
                    builder.Append("0");
                    i--;
                }
                builder.Append(value);
            }
        }
        public class CP24Time2aElement
        {
            public byte[] value = new byte[3];

            public CP24Time2aElement(long timestamp)
            {
                var datetime = new DateTime(timestamp);
                var ms = datetime.Millisecond + 1000 * datetime.Second;

                value[0] = (byte)ms;
                value[1] = (byte)(ms >> 8);
                value[2] = (byte)datetime.Minute;
            }

            public CP24Time2aElement(int timeInMs)
            {
                var ms = timeInMs % 60000;
                value[0] = (byte)ms;
                value[1] = (byte)(ms >> 8);
                value[2] = (byte)(ms >> 8);
            }
            public CP24Time2aElement(byte[] buffer, int pos) //for read
            {
                for (int i = 0; i < 3; i++)
                {
                    value[i] = buffer[pos + i];
                }
            }
            public int byte_encode(byte[] buffer, int pos)
            {
                Array.Copy(value, 0, buffer, pos, 3);
                return 3;
            }
            public int GetTimeInMs()
            {
                return (value[0] & 0xff) + ((value[1] & 0xff) << 8) + value[2] * 6000;
            }

        }
        public class CP16Time2aElement
        {
            public byte[] value = new byte[2];

            public CP16Time2aElement(long timestamp)
            {
                var datetime = new DateTime(timestamp);
                var ms = datetime.Millisecond + 1000 * datetime.Second;

                value[0] = (byte)ms;
                value[1] = (byte)(ms >> 8);
            }

            public CP16Time2aElement(int timeInMs)
            {
                var ms = timeInMs % 60000;
                value[0] = (byte)ms;
                value[1] = (byte)(ms >> 8);
            }

            public CP16Time2aElement(byte[] buff, int pos)
            {
                for (int i = 0; i < 2; i++)
                {
                    value[i] = buff[i + pos];
                }
            }

            public int byte_encode(byte[] buffer, int pos)
            {
                Array.Copy(value, 0, buffer, pos, 2);
                return 2;
            }

            public int GetTimeInMs()
            {
                return (value[0] & 0xff) + ((value[1] & 0xff) << 8);
            }

        }
        /*Information object type definition and parse*/

        public class InformationObjBase
        {
            protected IEC104_Setting setting = null;
            protected int ioa = 0;
            public InformationObjBase(IEC104_Setting setting, int ioa)
            {
                this.setting = setting;
                this.ioa = ioa;
            }
            public virtual int byte_encode(byte[] buff, int pos) { return 0; }
            public virtual int getBytesLen() { return -1; }

        }

        public class C_RD_NA_1_ReadIOACmd : InformationObjBase
        {

            public C_RD_NA_1_ReadIOACmd(IEC104_Setting set, int ioa)
                : base(set, ioa)
            {
             // do nothing
            }
            public override int getBytesLen()
            {
                return setting.ioa_size + 1;
            }
            public override int byte_encode(byte[] buff, int pos)
            {
                //int len = 0;
                switch (setting.ioa_size)
                {
                    case 1:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        break;
                    case 2:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        break;
                    case 3:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                        break;
                    default:
                        break;
                }
                return (setting.ioa_size);
            }
        }

        public class C_CI_NA_1_CounterInterrogationCMD : InformationObjBase
        {
            //QOI_QualifierOfInterrogationElement QOI;
            QCC_QualifierOfCounterInterrogationElement QCC;
            public C_CI_NA_1_CounterInterrogationCMD(IEC104_Setting set, int ioa, QCC_QualifierOfCounterInterrogationElement QCC)
                : base(set, ioa)
            {
                this.QCC = QCC;
            }
            public override int getBytesLen()
            {
                return setting.ioa_size + 1;
            }
            public override int byte_encode(byte[] buff, int pos)
            {
                int len = 0;
                switch (setting.ioa_size)
                {
                    case 1:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        break;
                    case 2:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        break;
                    case 3:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                        break;
                    default:
                        break;
                }
                //len = QOI.byte_encode(buff, pos);
                len = QCC.byte_encode(buff, pos);
                return (setting.ioa_size + len);
            }
        }


        //========================

        public class C_RP_NA_1_ResetCMD : InformationObjBase
        {
            QRP_QualifierOfResetProcessElement QRP;
            public C_RP_NA_1_ResetCMD(IEC104_Setting set, int ioa, QRP_QualifierOfResetProcessElement QRP)
                : base(set, ioa)
            {
                this.QRP = QRP;
            }
            public override int getBytesLen()
            {
                return setting.ioa_size + 1;
            }
            public override int byte_encode(byte[] buff, int pos)
            {
                int len = 0;
                switch (setting.ioa_size)
                {
                    case 1:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        break;
                    case 2:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        break;
                    case 3:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                        break;
                    default:
                        break;
                }
                len = QRP.byte_encode(buff, pos);
                return (setting.ioa_size + len);
            }
        }


        //========================

        public class C_IC_NA_1_InterrogationCMD : InformationObjBase
        {
            QOI_QualifierOfInterrogationElement QOI;
            public C_IC_NA_1_InterrogationCMD(IEC104_Setting set, int ioa, QOI_QualifierOfInterrogationElement QOI)
                : base(set, ioa)
            {
                this.QOI = QOI;
            }
            public override int getBytesLen()
            {
                return setting.ioa_size + 1;
            }
            public override int byte_encode(byte[] buff, int pos)
            {
                int len = 0;
                switch (setting.ioa_size)
                {
                    case 1:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        break;
                    case 2:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        break;
                    case 3:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                        break;
                    default:
                        break;
                }
                len = QOI.byte_encode(buff, pos);
                return (setting.ioa_size + len);
            }
        }



        public class C_SC_NA_1_SinglePointCMD : InformationObjBase
        {
            QOI_QualifierOfInterrogationElement QOI;
            SCO_SingleCommandElement SCO;
            public C_SC_NA_1_SinglePointCMD(IEC104_Setting set, int ioa, SCO_SingleCommandElement SCO)
                : base(set, ioa)
            {
                this.SCO = SCO;
            }
            public override int getBytesLen()
            {
                return setting.ioa_size + 1;
            }
            public override int byte_encode(byte[] buff, int pos)
            {
                int len = 0;
                switch (setting.ioa_size)
                {
                    case 1:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        break;
                    case 2:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        break;
                    case 3:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                        break;
                    default:
                        break;
                }
                len = SCO.byte_encode(buff, pos);
                return (setting.ioa_size + len);
            }
        }

        public class C_DC_NA_1_DoublePointCMD : InformationObjBase
        {
            QOI_QualifierOfInterrogationElement QOI;
            DCO_DoubleCommandElement DCO;
            public C_DC_NA_1_DoublePointCMD(IEC104_Setting set, int ioa, DCO_DoubleCommandElement DCO)
                : base(set, ioa)
            {
                this.DCO = DCO;
            }
            public override int getBytesLen()
            {
                return setting.ioa_size + 1;
            }
            public override int byte_encode(byte[] buff, int pos)
            {
                int len = 0;
                switch (setting.ioa_size)
                {
                    case 1:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        break;
                    case 2:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        break;
                    case 3:
                        buff[pos++] = (byte)(ioa & 0xFF);
                        buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                        buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                        break;
                    default:
                        break;
                }
                len = DCO.byte_encode(buff, pos);
                return (setting.ioa_size + len);
            }
        }


        public class M_SP_NA_1_SinglePointWithoutTime : InformationObjBase
        {
            public int numberIO = 1;
            public bool sq_bit = false;
            public SIQ_SinglePointInformationElement SIQ;
            //for parse result
            public List<SIQ_SinglePointInformationElement> SIQ_list = new List<SIQ_SinglePointInformationElement>();
            public List<int> ioa_list = new List<int>();

            public M_SP_NA_1_SinglePointWithoutTime(IEC104_Setting set, int ioa, SIQ_SinglePointInformationElement siq)
                : base(set, ioa)
            {
                this.SIQ_list.Clear();
                this.ioa_list.Clear();
                this.SIQ = siq;
            }
            //for readder and parse
            public M_SP_NA_1_SinglePointWithoutTime(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                this.sq_bit = sq_bit;
                this.numberIO = numberIO;

                SIQ_list.Clear();
                ioa_list.Clear();
                if (sq_bit)//sequence 
                {
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                    ioa_list.Add(ioa);

                    for (int i = 1; i < numberIO; i++)
                    {
                        SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                        ioa_list.Add(ioa + i);
                    }
                }
                else //separate
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos) // for only single IOA
            {

                int len = 0;
                if (sq_bit == false)
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                }
                else
                {
                    throw new Exception("Not support byte encode for option SQ=1");
                }

                len += SIQ.byte_encode(buff, pos);
                return (len);
            }
        }
        public class M_SP_TB_1_SinglePointWithTime56 : InformationObjBase
        {
            bool sq_bit = false;//allways false incase use with timetag
            public SIQ_SinglePointInformationElement SIQ;
            public CP56Time2aElement TIME_TAG;

            //for ret result
            public List<SIQ_SinglePointInformationElement> SIQ_list = new List<SIQ_SinglePointInformationElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public List<int> ioa_list = new List<int>();

            public M_SP_TB_1_SinglePointWithTime56(IEC104_Setting set, int ioa, bool sq_bit, SIQ_SinglePointInformationElement siq, CP56Time2aElement timetag)
                : base(set, ioa)
            {
                SIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    this.sq_bit = sq_bit;
                    this.SIQ = siq;
                    this.TIME_TAG = timetag;
                }
            }
            //for readder and parse
            public M_SP_TB_1_SinglePointWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                SIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();

                if (sq_bit)
                {
                    // throw new Exception("Can not use SQ=true with TimeTag option");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);

                    SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos += 7;//move to next IO

                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos += 7;//move to next IO

                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos += 7;//move to next IO

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos)
            {

                int len = 0;
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                    len += SIQ.byte_encode(buff, pos++);
                    len += TIME_TAG.byte_encode(buff, pos);
                }


                return (len);
            }
        }
        public class M_SP_TA_1_SinglePointWithTime24 : InformationObjBase
        {
            bool sq_bit = false;//allways false incase use with timetag
            public SIQ_SinglePointInformationElement SIQ;
            public CP24Time2aElement TIME_TAG;

            //for ret result
            public List<SIQ_SinglePointInformationElement> SIQ_list = new List<SIQ_SinglePointInformationElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public List<int> ioa_list = new List<int>();

            public M_SP_TA_1_SinglePointWithTime24(IEC104_Setting set, int ioa, bool sq_bit, SIQ_SinglePointInformationElement siq, CP24Time2aElement timetag)
                : base(set, ioa)
            {
                SIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    this.sq_bit = sq_bit;
                    this.SIQ = siq;
                    this.TIME_TAG = timetag;
                }
            }
            //for readder and parse
            public M_SP_TA_1_SinglePointWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                SIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();

                if (sq_bit)
                {
                    //throw new Exception("Can not use SQ=true with TimeTag option");

                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);
                    SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos += 3;//move to next IO
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos += 3;//move to next IO

                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        SIQ_list.Add(new SIQ_SinglePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos += 3;//move to next IO

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos)
            {

                int len = 0;
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                    len += SIQ.byte_encode(buff, pos++);
                    len += TIME_TAG.byte_encode(buff, pos);
                }


                return (len);
            }
        }

        public class M_DP_NA_1_DoublePointWithoutTime : InformationObjBase
        {
            public int numberIO = 1;
            public bool sq_bit = false;
            public DIQ_DoublePointInformationElement DIQ;
            //for parse result
            public List<DIQ_DoublePointInformationElement> DIQ_list = new List<DIQ_DoublePointInformationElement>();
            public List<int> ioa_list = new List<int>();

            public M_DP_NA_1_DoublePointWithoutTime(IEC104_Setting set, int ioa, DIQ_DoublePointInformationElement diq)
                : base(set, ioa)
            {
                this.DIQ_list.Clear();
                this.ioa_list.Clear();
                this.DIQ = diq;
            }
            //for readder and parse
            public M_DP_NA_1_DoublePointWithoutTime(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                this.sq_bit = sq_bit;
                this.numberIO = numberIO;

                DIQ_list.Clear();
                ioa_list.Clear();
                if (sq_bit)//sequence 
                {
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                    ioa_list.Add(ioa);

                    for (int i = 1; i < numberIO; i++)
                    {
                        DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                        ioa_list.Add(ioa + i);
                    }
                }
                else //separate
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos) // for only single IOA
            {

                int len = 0;
                if (sq_bit == false)
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                }
                else
                {
                    throw new Exception("Not support byte encode for option SQ=1");
                }

                len += DIQ.byte_encode(buff, pos);
                return (len);
            }
        }
        public class M_DP_TB_1_DoublePointWithTime56 : InformationObjBase
        {
            bool sq_bit = false;//allways false incase use with timetag
            public DIQ_DoublePointInformationElement DIQ;
            public CP56Time2aElement TIME_TAG;

            //for ret result
            public List<DIQ_DoublePointInformationElement> DIQ_list = new List<DIQ_DoublePointInformationElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public List<int> ioa_list = new List<int>();

            public M_DP_TB_1_DoublePointWithTime56(IEC104_Setting set, int ioa, bool sq_bit, DIQ_DoublePointInformationElement diq, CP56Time2aElement timetag)
                : base(set, ioa)
            {
                DIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    this.sq_bit = sq_bit;
                    this.DIQ = diq;
                    this.TIME_TAG = timetag;
                }
            }
            //for readder and parse
            public M_DP_TB_1_DoublePointWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                DIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();

                if (sq_bit)
                {
                    // throw new Exception("Can not use SQ=true with TimeTag option");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);

                    DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos += 7;//move to next IO

                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos += 7;//move to next IO

                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos += 7;//move to next IO

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos)
            {

                int len = 0;
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                    len += DIQ.byte_encode(buff, pos++);
                    len += TIME_TAG.byte_encode(buff, pos);
                }


                return (len);
            }
        }
        public class M_DP_TA_1_DoublePointWithTime24 : InformationObjBase
        {
            bool sq_bit = false;//allways false incase use with timetag
            public DIQ_DoublePointInformationElement DIQ;
            public CP24Time2aElement TIME_TAG;

            //for ret result
            public List<DIQ_DoublePointInformationElement> DIQ_list = new List<DIQ_DoublePointInformationElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public List<int> ioa_list = new List<int>();

            public M_DP_TA_1_DoublePointWithTime24(IEC104_Setting set, int ioa, bool sq_bit, DIQ_DoublePointInformationElement diq, CP24Time2aElement timetag)
                : base(set, ioa)
            {
                DIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    this.sq_bit = sq_bit;
                    this.DIQ = diq;
                    this.TIME_TAG = timetag;
                }
            }
            //for readder and parse
            public M_DP_TA_1_DoublePointWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                DIQ_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();

                if (sq_bit)
                {
                    //throw new Exception("Can not use SQ=true with TimeTag option");

                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);
                    DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos += 3;//move to next IO
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos += 3;//move to next IO

                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        DIQ_list.Add(new DIQ_DoublePointInformationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos += 3;//move to next IO

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos)
            {

                int len = 0;
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                    len += DIQ.byte_encode(buff, pos++);
                    len += TIME_TAG.byte_encode(buff, pos);
                }


                return (len);
            }
        }

        public class M_ST_NA_1_StepPositionInformationWithoutTime : InformationObjBase
        {
            public int numberIO = 1;
            public bool sq_bit = false;
            public VTI_ValueWithTransientStateIndicationElement VTI;
            //for parse result
            public List<VTI_ValueWithTransientStateIndicationElement> VTI_list = new List<VTI_ValueWithTransientStateIndicationElement>();
            public List<int> ioa_list = new List<int>();

            public M_ST_NA_1_StepPositionInformationWithoutTime(IEC104_Setting set, int ioa, VTI_ValueWithTransientStateIndicationElement vti)
                : base(set, ioa)
            {
                this.VTI_list.Clear();
                this.ioa_list.Clear();
                this.VTI = vti;
            }
            //for readder and parse
            public M_ST_NA_1_StepPositionInformationWithoutTime(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                this.sq_bit = sq_bit;
                this.numberIO = numberIO;

                VTI_list.Clear();
                ioa_list.Clear();
                if (sq_bit)//sequence 
                {
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                    ioa_list.Add(ioa);

                    for (int i = 1; i < numberIO; i++)
                    {
                        VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                        ioa_list.Add(ioa + i);
                    }
                }
                else //separate
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos) // for only single IOA
            {

                int len = 0;
                if (sq_bit == false)
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                }
                else
                {
                    throw new Exception("Not support byte encode for option SQ=1");
                }

                len += VTI.byte_encode(buff, pos);
                return (len);
            }
        }
        public class M_ST_TB_1_StepPositionInformationWithTime56 : InformationObjBase
        {
            bool sq_bit = false;//allways false incase use with timetag
            public VTI_ValueWithTransientStateIndicationElement VTI;
            public CP56Time2aElement TIME_TAG;

            //for ret result
            public List<VTI_ValueWithTransientStateIndicationElement> VTI_list = new List<VTI_ValueWithTransientStateIndicationElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public List<int> ioa_list = new List<int>();

            public M_ST_TB_1_StepPositionInformationWithTime56(IEC104_Setting set, int ioa, bool sq_bit, VTI_ValueWithTransientStateIndicationElement vti, CP56Time2aElement timetag)
                : base(set, ioa)
            {
                VTI_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    this.sq_bit = sq_bit;
                    this.VTI = vti;
                    this.TIME_TAG = timetag;
                }
            }
            //for readder and parse
            public M_ST_TB_1_StepPositionInformationWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                VTI_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();

                if (sq_bit)
                {
                    // throw new Exception("Can not use SQ=true with TimeTag option");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);

                    VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos += 7;//move to next IO

                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos += 7;//move to next IO

                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos += 7;//move to next IO

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos)
            {

                int len = 0;
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                    len += VTI.byte_encode(buff, pos++);
                    len += TIME_TAG.byte_encode(buff, pos);
                }


                return (len);
            }
        }
        public class M_ST_TA_1_StepPositionInformationWithTime24 : InformationObjBase
        {
            bool sq_bit = false;//allways false incase use with timetag
            public VTI_ValueWithTransientStateIndicationElement VTI;
            public CP24Time2aElement TIME_TAG;

            //for ret result
            public List<VTI_ValueWithTransientStateIndicationElement> VTI_list = new List<VTI_ValueWithTransientStateIndicationElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public List<int> ioa_list = new List<int>();

            public M_ST_TA_1_StepPositionInformationWithTime24(IEC104_Setting set, int ioa, bool sq_bit, VTI_ValueWithTransientStateIndicationElement vti, CP24Time2aElement timetag)
                : base(set, ioa)
            {
                VTI_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    this.sq_bit = sq_bit;
                    this.VTI = vti;
                    this.TIME_TAG = timetag;
                }
            }
            //for readder and parse
            public M_ST_TA_1_StepPositionInformationWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                VTI_list.Clear();
                TimeTag_list.Clear();
                ioa_list.Clear();

                if (sq_bit)
                {
                    //throw new Exception("Can not use SQ=true with TimeTag option");

                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);
                    VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos += 3;//move to next IO
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos += 3;//move to next IO

                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {
                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }
                        ioa_list.Add(ioa);
                        VTI_list.Add(new VTI_ValueWithTransientStateIndicationElement(apdubuff[pos++]));
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos += 3;//move to next IO

                    }
                }
            }

            public override int byte_encode(byte[] buff, int pos)
            {

                int len = 0;
                if (sq_bit)
                {
                    throw new Exception("Can not use SQ=true with TimeTag option");
                }
                else
                {
                    switch (setting.ioa_size)
                    {
                        case 1:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            break;
                        case 2:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            break;
                        case 3:
                            buff[pos++] = (byte)(ioa & 0xFF);
                            buff[pos++] = (byte)((ioa >> 8) & 0xFF);
                            buff[pos++] = (byte)((ioa >> 16) & 0xFF);
                            break;
                        default:
                            break;
                    }
                    len = setting.ioa_size;
                    len += VTI.byte_encode(buff, pos++);
                    len += TIME_TAG.byte_encode(buff, pos);
                }


                return (len);
            }
        }

        public class M_ME_NA_1_MeasuredNormalizedValue : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<NVA_NormalizedValueInformationElement> value_list = new List<NVA_NormalizedValueInformationElement>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public M_ME_NA_1_MeasuredNormalizedValue(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                this.sq_bit = sq_bit;

                int ex = 1;
                ioa = 0;
                for (int i = 0; i < set.ioa_size; i++)
                {
                    ioa += apdubuff[pos++] * ex;
                    ex = ex * 256;
                }

                ioa_list.Add(ioa);
                value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                pos += 2;// nva is 2 byte
                quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                pos += 1;//qds size =1

                for (int k = 1; k < numberIO; k++)
                {
                    if (sq_bit)//sequence bit
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                    }
                    else
                    {
                        ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                    }

                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_ME_TA_1_MeasuredNormalizedValueWithTime24 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<NVA_NormalizedValueInformationElement> value_list = new List<NVA_NormalizedValueInformationElement>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public M_ME_TA_1_MeasuredNormalizedValueWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    // throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);
                    value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                    pos += 2;// nva is 2 byte
                    quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                    pos += 1;//qds size =1                    
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos = pos + 3;///time tag  size =3
                    ///
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }
                else
                {

                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_ME_TD_1_MeasuredNormalizedValueWithTime56 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<NVA_NormalizedValueInformationElement> value_list = new List<NVA_NormalizedValueInformationElement>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public M_ME_TD_1_MeasuredNormalizedValueWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    //throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }
                    ioa_list.Add(ioa);
                    value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                    pos += 2;// nva is 2 byte
                    quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                    pos += 1;//qds size =1                    
                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos = pos + 7;///time tag  size =7
                    ///
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }
                else
                {

                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }

        public class M_ME_NB_1_MeasuredScaledValue : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<SVA_ScaledValueInformationElement> value_list = new List<SVA_ScaledValueInformationElement>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public M_ME_NB_1_MeasuredScaledValue(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                this.sq_bit = sq_bit;

                int ex = 1;
                ioa = 0;
                for (int i = 0; i < set.ioa_size; i++)
                {
                    ioa += apdubuff[pos++] * ex;
                    ex = ex * 256;
                }

                ioa_list.Add(ioa);
                value_list.Add(new SVA_ScaledValueInformationElement(apdubuff, pos));
                pos += 2;// sva is 2 byte
                quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                pos += 1;//qds size =1

                for (int k = 1; k < numberIO; k++)
                {
                    if (sq_bit)//sequence bit
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new SVA_ScaledValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                    }
                    else
                    {
                        ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new SVA_ScaledValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                    }

                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_ME_TB_1_MeasuredScaledValueWithTime24 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<NVA_NormalizedValueInformationElement> value_list = new List<NVA_NormalizedValueInformationElement>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public M_ME_TB_1_MeasuredScaledValueWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    // throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }

                    ioa_list.Add(ioa);
                    value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                    pos += 2;// nva is 2 byte
                    quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                    pos += 1;//qds size =1                    
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos = pos + 3;///time tag  size =3

                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_ME_TE_1_MeasuredScaledValueWithTime56 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<NVA_NormalizedValueInformationElement> value_list = new List<NVA_NormalizedValueInformationElement>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public M_ME_TE_1_MeasuredScaledValueWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    // throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }

                    ioa_list.Add(ioa);
                    value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                    pos += 2;// nva is 2 byte
                    quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                    pos += 1;//qds size =1                    
                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos = pos + 7;///time tag  size =7
                    ///
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new NVA_NormalizedValueInformationElement(apdubuff, pos));
                        pos += 2;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }

        public class M_ME_NC_1_MeasuredShortFloatingPointValue : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<R32_ShortFloatingPointNumber> value_list = new List<R32_ShortFloatingPointNumber>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public M_ME_NC_1_MeasuredShortFloatingPointValue(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                this.sq_bit = sq_bit;

                int ex = 1;
                ioa = 0;
                for (int i = 0; i < set.ioa_size; i++)
                {
                    ioa += apdubuff[pos++] * ex;
                    ex = ex * 256;
                }

                ioa_list.Add(ioa);
                value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                pos += 4;// nva is 4 byte
                quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                pos += 1;//qds size =1

                for (int k = 1; k < numberIO; k++)
                {
                    if (sq_bit)//sequence bit
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                        pos += 4;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                    }
                    else
                    {
                        ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                        pos += 4;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                    }

                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_ME_TC_1_MeasuredShortFloatingPointWithTime24 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<R32_ShortFloatingPointNumber> value_list = new List<R32_ShortFloatingPointNumber>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public M_ME_TC_1_MeasuredShortFloatingPointWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    // throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }

                    ioa_list.Add(ioa);
                    value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                    pos += 4;// nva is 2 byte
                    quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                    pos += 1;//qds size =1                    
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos = pos + 3;///time tag  size =3
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                        pos += 4;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                        pos += 4;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_ME_TF_1_MeasuredShortFloatingPointWithTime56 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<R32_ShortFloatingPointNumber> value_list = new List<R32_ShortFloatingPointNumber>();
            public List<QDS_QualifierOfDescriptorElement> quality_descriptor_list = new List<QDS_QualifierOfDescriptorElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public M_ME_TF_1_MeasuredShortFloatingPointWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                quality_descriptor_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    //throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }

                    ioa_list.Add(ioa);
                    value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                    pos += 4;// nva is 2 byte
                    quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                    pos += 1;//qds size =1                    
                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos = pos + 7;///time tag  size =7
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                        pos += 4;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new R32_ShortFloatingPointNumber(apdubuff, pos));
                        pos += 4;// nva is 2 byte
                        quality_descriptor_list.Add(new QDS_QualifierOfDescriptorElement(apdubuff[pos]));
                        pos += 1;//qds size =1                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }

        public class M_IT_NA_1_IntegratedTotalsValue : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;
            public List<int> ioa_list = new List<int>();
            public List<BCR_BinaryCounterReadingElement> value_list = new List<BCR_BinaryCounterReadingElement>();
            public M_IT_NA_1_IntegratedTotalsValue(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();

                this.sq_bit = sq_bit;

                int ex = 1;
                ioa = 0;
                for (int i = 0; i < set.ioa_size; i++)
                {
                    ioa += apdubuff[pos++] * ex;
                    ex = ex * 256;
                }

                ioa_list.Add(ioa);
                value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                pos += 5;// BCR_BinaryCounterReadingElement is 5 byte
                for (int k = 1; k < numberIO; k++)
                {
                    if (sq_bit)//sequence bit
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                        pos += 5;// BCR_BinaryCounterReadingElement is 5 byte
                    }
                    else
                    {
                        ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                        pos += 5;// BCR_BinaryCounterReadingElement is 5 byte                                       
                    }

                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_IT_TB_1_IntegratedTotalsValuetWithTime24 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<BCR_BinaryCounterReadingElement> value_list = new List<BCR_BinaryCounterReadingElement>();
            public List<CP24Time2aElement> TimeTag_list = new List<CP24Time2aElement>();
            public M_IT_TB_1_IntegratedTotalsValuetWithTime24(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {

                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }

                    ioa_list.Add(ioa);
                    value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                    pos += 5;//5 byte BCR_BinaryCounterReadingElement                         
                    TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                    pos = pos + 3;///time tag  size =3
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                        pos += 5;//5 byte BCR_BinaryCounterReadingElement                                   
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                        pos += 5;// 5 byte                                    
                        TimeTag_list.Add(new CP24Time2aElement(apdubuff, pos));
                        pos = pos + 3;///time tag  size =3
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }
        public class M_IT_TA_1_IntegratedTotalsValueWithTime56 : InformationObjBase
        {
            // this class is implement just for read and parse data from apdu buffer
            bool sq_bit = false;//allways false incase use with timetag
            public List<int> ioa_list = new List<int>();
            public List<BCR_BinaryCounterReadingElement> value_list = new List<BCR_BinaryCounterReadingElement>();
            public List<CP56Time2aElement> TimeTag_list = new List<CP56Time2aElement>();
            public M_IT_TA_1_IntegratedTotalsValueWithTime56(IEC104_Setting set, bool sq_bit, int numberIO, byte[] apdubuff, int pos)
                : base(set, 0)
            {
                ioa_list.Clear();
                value_list.Clear();
                TimeTag_list.Clear();
                this.sq_bit = sq_bit;

                if (sq_bit == true)
                {
                    //throw new Exception("SQ=1 do not use with timetag");
                    int ex = 1;
                    ioa = 0;
                    for (int i = 0; i < set.ioa_size; i++)
                    {
                        ioa += apdubuff[pos++] * ex;
                        ex = ex * 256;
                    }

                    ioa_list.Add(ioa);
                    value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                    pos += 5;// 5 byte

                    TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                    pos = pos + 7;///time tag  size =7
                    for (int k = 1; k < numberIO; k++)
                    {
                        ioa_list.Add(ioa + k);
                        value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                        pos += 5;// 5 byte                                    
                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }
                else
                {
                    for (int k = 0; k < numberIO; k++)
                    {

                        int ex = 1;
                        ioa = 0;
                        for (int i = 0; i < set.ioa_size; i++)
                        {
                            ioa += apdubuff[pos++] * ex;
                            ex = ex * 256;
                        }

                        ioa_list.Add(ioa);
                        value_list.Add(new BCR_BinaryCounterReadingElement(apdubuff, pos));
                        pos += 5;// nva is 5 byte

                        TimeTag_list.Add(new CP56Time2aElement(apdubuff, pos));
                        pos = pos + 7;///time tag  size =7
                    }
                }

            }
            public override int byte_encode(byte[] buff, int pos) { return 0; }//not implement yet

        }

    }
}
