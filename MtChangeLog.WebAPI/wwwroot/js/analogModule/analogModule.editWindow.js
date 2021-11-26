class AnalogModuleEditWindow{
    constructor(editableObj){
        let _editableObj = editableObj;
        if(_editableObj == undefined || _editableObj == null){
            throw new Error("не выбран аналоговый модуль для работы");
        }
        
        let _uiWindow;
        this.show = function(){
            webix.ready(function(){
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        };

        let window = function () {
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:600,
                height:400,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };

        let headLayout = function () {
            let result = {
                view:"toolbar",
                id:"headToolbar",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Аналоговый модуль:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"btn_oscWinClose",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function(){
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function(){
            let lWidth = 180;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text",
                        label:"Децимальный номер:", 
                        labelAlign:"right", 
                        labelWidth:lWidth,
                        value:_editableObj.getDivg(),
                        attributes:{
                            maxlength:13,
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setDivg(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    { 
                        view:"text", 
                        label:"Наименование модуля:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getTitle(),
                        attributes:{
                            maxlength:8,
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setTitle(newValue);    
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    { 
                        view:"text", 
                        label:"Номинальный ток:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getNominalCurrent(),
                        attributes:{
                            maxlength:2,
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setNominalCurrent(newValue);    
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    { 
                        view:"textarea", 
                        label:"Описание:", 
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:150, 
                        value:_editableObj.getDescription(),
                        attributes:{
                            maxlength:200,
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setDescription(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"multicombo", 
                        id:"modulePlatforms_id",
                        label:"Модули:",
                        labelAlign:"right",
                        labelWidth:lWidth,  
                        value:_editableObj.getPlatforms(),
                        button:true,
                        suggest:{
                            body:{
                                data:_editableObj.getAllPlatforms(),
                                template:webix.template("#title#")
                            }
                        }
                    },
                    {
                        view:"button",
                        id:"submitButton_id",
                        value:"Сохронить",
                        css:"webix_primary",
                        inputWidth:200,
                        align:"right",
                        click: async function(){
                            try {
                                // обновить перечень платформ у модуля:
                                let selected = $$("modulePlatforms_id").getValue().split(",");
                                _editableObj.setPlatforms(selected);
                                
                                // отправить:
                                await _editableObj.submit();

                                // автоматически закрывать при удачном стечении обстоятельств:
                                _uiWindow.close();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally{
                                
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}