class CommunicationModuleEditWindow {
    constructor(editable){
        
        let _editable = editable;
        if(_editable == undefined || _editable == null){
            throw new Error("не выбран коммуникационный модуль для работы");
        }

        let _uiWindow;
        this.show = function(){
            webix.ready(function (){
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        let window = function (){
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:600,
                height:600,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };
        
        let headLayout = function (){
            let result = {
                view:"toolbar",
                id:"headToolbar",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Коммуникационный модуль:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"btn_winClose",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function (){
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function (){
            let lWidth = 160;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Наименование:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editable.getTitle(),
                        attributes:{
                            maxlength:255
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editable.setTitle(newValue);    
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
                        height:120,
                        value:_editable.getDescription(),
                        attributes:{
                            maxlength:500
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editable.setDescription(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    
                    {
                        view:"multicombo", 
                        id:"moduleProtocols_id",
                        label:"Протоколы:",
                        labelAlign:"right",
                        labelWidth:lWidth,  
                        value:_editable.getProtocols(),
                        button:true,
                        suggest:{
                            body:{
                                data:_editable.getAllProtocols(),
                                template:webix.template("#title#")
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
                        click: async function(){
                            try{ 
                                // обновить перечень платформ у модуля:
                                let selected = $$("moduleProtocols_id").getValue().split(",");
                                _editable.setProtocols(selected);
                                
                                // отправить:
                                await _editable.submit();

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