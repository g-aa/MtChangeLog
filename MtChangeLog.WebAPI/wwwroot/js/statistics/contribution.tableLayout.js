class ContributionTableLayout{
    constructor(){
        let tableLayoutId = "contributionTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayoutId,
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                { id:"author",          width:250,   minWidth:150,   header:["Автор:", { content:"multiSelectFilter" }] },
                { id:"projectPrefix",    width:170,   header:["Аналоговый модуль:", { content:"multiSelectFilter" }] },
                { id:"projectTitle",    width:170,   header:["Наименование проекта:", { content:"multiSelectFilter" }] },
                { id:"projectVersion",  width:170,   header:["Версия проекта:", { content:"multiSelectFilter" }] },
                { id:"contribution",    width:170,   header:["Влад:", { content:"multiSelectFilter" }] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getAuthorProjectContributions();
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