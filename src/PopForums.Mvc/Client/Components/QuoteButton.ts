﻿namespace PopForums {

    export class QuoteButton extends HTMLElement {
    constructor() {
        super();
    }

    get name(): string {
        return this.getAttribute("name");
    }
    get containerid(): string {
        return this.getAttribute("containerid");
    }
    get buttonclass(): string {
        return this.getAttribute("buttonclass");
    }
    get buttontext(): string {
        return this.getAttribute("buttontext");
    }
    get tip(): string {
        return this.getAttribute("tip");
    }
    get postID(): string {
        return this.getAttribute("postid");
    }

    private _tip: any;

    connectedCallback() {
        let targetText = document.getElementById(this.containerid);
        this.innerHTML = QuoteButton.template;
        let button = this.querySelector("button");
        button.title = this.tip;
        ["mousedown","touchstart"].forEach((e:string) => 
            targetText.addEventListener(e, () => 
                { if (this._tip) this._tip.hide() }));
        button.value = this.buttontext;
        let classes = this.buttonclass;
        if (classes?.length > 0)
            classes.split(" ").forEach((c) => button.classList.add(c));
        this.onclick = (e: MouseEvent) => {
            // get this from topic state's callback/ready method, because iOS loses selection when you touch quote button
            let fragment = PopForums.currentTopicState.documentFragment;
            let ancestor = PopForums.currentTopicState.selectionAncestor;
            if (!fragment) {
                let selection = document.getSelection();
                if (!selection || selection.rangeCount === 0 || selection.getRangeAt(0).toString().length === 0) {
                    // prompt to select
                    this._tip = new bootstrap.Tooltip(button, {trigger: "manual"});
                    this._tip.show();
                    return;
                }
                let range = selection.getRangeAt(0);
                ancestor = range.commonAncestorContainer;
                fragment = range.cloneContents();
            }
            let div = document.createElement("div");
            div.appendChild(fragment);
            // is selection in the container?
            while (ancestor['id'] !== this.containerid && ancestor.parentElement !== null) {
                ancestor = ancestor.parentElement;
            }
            let isInText = ancestor['id'] === this.containerid;
            // if not, is it partially in the container?
            if (!isInText) {
                let container = div.querySelector("#" + this.containerid);
                if (container !== null && container !== undefined) {
                    // it's partially in the container, so just get that part
                    div.innerHTML = container.innerHTML;
                    isInText = true;
                }
            }
            if (isInText) {
                // activate or add to quote
                let result: string;
                if (userState.isPlainText)
                    result = `[quote][i]${this.name}:[/i]\r\n ${div.innerText}[/quote]`;
                else
                    result = `<blockquote><p><i>${this.name}:</i></p>${div.innerHTML}</blockquote><p></p>`;
                PopForums.currentTopicState.nextQuote = result;
                if (!PopForums.currentTopicState.isReplyLoaded)
                    PopForums.currentTopicState.loadReply(PopForums.currentTopicState.topicID, Number(this.postID), true);
            }
            let temp = document.getSelection();
            if (temp)
                temp.removeAllRanges();
        };
    }

    static template: string = `<button type="button" data-bs-toggle="tooltip" title="" />`;
}

customElements.define('pf-quotebutton', QuoteButton);

}