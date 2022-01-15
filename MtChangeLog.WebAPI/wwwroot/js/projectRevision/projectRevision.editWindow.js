class ProjectRevisionEditWindow {
    constructor(editableObj) {
        let _editableObj = editableObj;
        if(!_editableObj instanceof ProjectRevision){
            throw new Error("не выбрана ревизия БФПО для работы");
        }
        
        let _uiWindow;
        this.show = function() {
            webix.ready(function () {
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        // запуск прогрес бара при выполнении операций:
        let showProgress = function(){
            _uiWindow.disable();
            webix.extend(_uiWindow, webix.ProgressBar);
            _uiWindow.showProgress({ type:"icon" });
        }

        // остановка прогрес бара:
        let closeProgress = function(){
            _uiWindow.hideProgress();
            _uiWindow.enable();
        }

        let window = function () {
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:800,
                //height:800,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };

        let headLayout = function () {
            let result = {
                view:"toolbar",
                id:"headToolbar_id",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Редакция БФПО:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"winCloseBtn_id",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function () {
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function () {
            let lWidth = 230; // label width
            let dHeight = 300; // description height
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    {
                        view:"richselect",
                        id:"parentRevision_id",
                        label:"Предыдущая редакция БФПО:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        icon:"mdi mdi-alpha-r-box",
                        value:_editableObj.getParentRevision(),
                        options:{
                            body:{
                                template:"#module#-#title#-#version#_#revision#"
                            },   
                            data:_editableObj.getAllParentRevisions()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try {
                                    _editableObj.setParentRevision(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"richselect",
                        id:"projectVersion_id",
                        label:"Версия БФПО:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        icon:"mdi mdi-alpha-v-box",
                        value:_editableObj.getProjectVersion(),
                        options:{
                            body:{
                                template:"#module#-#title#-#version#"
                            },   
                            data:_editableObj.getAllProjectVersions()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try {
                                    _editableObj.setProjectVersion(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"text",
                        id:"revision_id", 
                        label:"Номер текущей редакции:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getRevision(),
                        //tooltip:"Type in to search",
                        attributes:{
                            maxlength:2
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                try {
                                    _editableObj.setRevision(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"datepicker",
                        id:"revisionDate_id",
                        label:'Дата редакции:',
                        labelAlign:"right",
                        labelWidth:lWidth,
                        timepicker:false,
                        format:"%Y-%m-%d",
                        value:_editableObj.getDate(),
                        on:{
                            onChange: function(newValue, oldValue, config){
                                try {
                                    _editableObj.setDate(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"richselect",
                        id:"revisionArmEdit_id",
                        label:"Версия ArmEdit:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        icon:"mdi mdi-application-brackets",
                        value:_editableObj.getArmEdit(),
                        options:{
                            body:{
                                template:"#version#"
                            },   
                            data:_editableObj.getAllArmEdits()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try {
                                    _editableObj.setArmEdit(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"multicombo",
                        id:"revisionAuthors_id",
                        label:"Автор редакции:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getAuthors(),
                        button:true,
                        suggest:{
                            body:{
                                data:_editableObj.getAllAuthors(),
                                template:webix.template("#lastName# #firstName#")
                            }
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                // обновление значений выполняется перед отправлением
                            }
                        }
                    },
                    {
                        view:"multicombo",
                        id:"revisionAlgorithms_id",
                        label:"Алгоритмы РЗиА:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getAlgorithms(),
                        button:true,
                        suggest:{
                            body:{
                                data:_editableObj.getAllAlgorithms(),
                                template:webix.template("#title#")
                            }
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                // обновление значений выполняется перед отправлением
                            }
                        }
                    },
                    {
                        view:"richselect",
                        id:"revisionCommunications_id",
                        label:"Протоколы:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        icon:"mdi mdi-ethernet",
                        value:_editableObj.getCommunicationModule(),
                        options:{
                            body:{
                                template:"#title#"
                            },   
                            data:_editableObj.getAllCommunicationModules()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try {
                                    _editableObj.setCommunicationModule(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"text",
                        id:"revisionReason_id", 
                        label:"Причина изменений:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getReason(),
                        attributes:{
                            maxlength:500
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try {
                                    _editableObj.setReason(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"textarea",
                        id:"revisionDescription_id",
                        label:"Описание:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:dHeight, 
                        value:_editableObj.getDescription(),
                        attributes:{
                            maxlength:5000
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                try {
                                    _editableObj.setDescription(newValue);
                                } catch (error) {
                                    messageBox.alertWarning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"button",
                        id:"submitButton_id",
                        value:"Отправить",
                        css:"webix_primary",
                        inputWidth:200,
                        align:"right",
                        click: async function (){
                            try{ 
                                showProgress();
                                // обновить перечень авторов:
                                let selectedAuthorsId = $$("revisionAuthors_id").getValue().split(",");
                                _editableObj.setAuthors(selectedAuthorsId);

                                // обновить перечень алгоритмов РЗиА:
                                let selectedAlgorithmsId = $$("revisionAlgorithms_id").getValue().split(",");
                                _editableObj.setAlgorithms(selectedAlgorithmsId);

                                // отправить:
                                await _editableObj.submit();

                                // автоматически закрывать при удачном стечении обстоятельств:
                                _uiWindow.close();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally{
                                closeProgress();
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}