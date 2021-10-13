class AuthorTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;
        this.tableLayout = {
            view:"datatable",
            id:"authorTable_id", 
            select:"row",
            adjust:true,
            resizeColumn:true,
            columns:[
                { id:"id",             adjust:true, header:["GUID:", ""] },
                { id:"firstName",           adjust:true, header:["Имя:", { content:"multiSelectFilter" }] },
                { id:"lastName",        adjust:true, header:["Фамилия:", { content:"multiSelectFilter" }] },
                { id:"position",    fillspace:true, header:["Должность:", { content:"multiSelectFilter" }] }
            ],
            data:[]
        }

        let btnWidth = 200;
        this.buttonsLayout = {
            view:"toolbar", 
            cols:[
                { 
                    view:"button", 
                    css:"webix_primary",
                    value:"Добавить", 
                    width:btnWidth,
                    click: async function (){
                        try{
                            let author = new Author();
                            await author.defaultInitialize();
                            author.beforeEnding = async function(url, answer){
                                messageBox.information(answer);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(url);
                                let table = $$("authorTable_id");
                                table.parse(tableData);
                                table.refresh();
                            };

                            let win = new AuthorEditWindow(author);
                            win.show();
                        } catch (error){
                            messageBox.error(error.message);
                        }
                    }
                },
                { 
                    view:"button", 
                    css:"webix_secondary",
                    value:"Редактировать", 
                    width:btnWidth,
                    click: async function (){
                        try{
                            let selected = $$("authorTable_id").getSelectedItem();
                            if(selected != undefined){
                                let author = new Author();
                                await author.initialize(selected);
                                author.beforeEnding = async function(url, answer){
                                    messageBox.information(answer);
                                
                                    let tableData = await entitiesRepository.getEntitiesInfo(url);
                                    let table = $$("authorTable_id");
                                    table.parse(tableData);
                                    table.refresh();
                                };

                                let win = new AuthorEditWindow(author);
                                win.show();
                            } else{
                                messageBox.information("выберете автора для редактирования");
                            }
                        } catch (error){
                            messageBox.error(error.message);
                        }
                    }
                },
                { 
                    view:"button",
                    css:"webix_danger",
                    value:"Удалить",
                    width:btnWidth,
                    align:"right",
                    click: async function (){
                        try{
                            
                        } catch (error){
                            messageBox.error(error.message); 
                        }
                    } 
                }
            ]
        }
    }

    show(){
        webix.ui({
            view:"layout",
            rows:[ this.buttonsLayout, this.tableLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);

        let url = entitiesRepository.getAuthorsUrl();
        entitiesRepository.getEntitiesInfo(url)
        .then(tableData => {
            let table = $$("authorTable_id");
            table.parse(tableData);
            table.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}