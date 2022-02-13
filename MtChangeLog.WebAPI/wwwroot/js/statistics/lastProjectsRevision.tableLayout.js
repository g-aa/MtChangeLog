class LastProjectRevisionTableLayout{
    constructor(){
        let tableLayoutId = "lastProjectRevisionTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayoutId,
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                { id:"prefix",      width:150,   minWidth:150,   header:["Префикс:", { content:"multiSelectFilter" }] },
                { id:"title",       adjust:true, header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"version",     width:100,   header:["Версия:", { content:"multiSelectFilter" }] },
                { id:"revision",    width:100,   header:["Ревизия:", { content:"multiSelectFilter" }] },
                { id:"date",        adjust:true, template:function(obj){ 
                        let format = webix.Date.dateToStr("%Y-%m-%d");
                        return format(new Date(obj.date)); 
                    }, header:["Дата:", ""] },
                { id:"platform",    width:150,   header:["Платформа:", { content:"multiSelectFilter" }] },
                { id:"armEdit",     width:170,   header:["ArmEdit:", { content:"multiSelectFilter" }] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getLastProjectsRevision();
            let tableLayout = $$(tableLayoutId);
            tableLayout.parse(tableData);
            tableLayout.refresh();
        }
        this.refresh = refresh;
    }

    async show(parentLayout){
        webix.ui(this.tableLayout, parentLayout.getChildViews()[0]);
        await this.refresh();
    }
}