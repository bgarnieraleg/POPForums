namespace PopForums {

export class PMForm extends HTMLElement {
    constructor() {
        super();
    }
    
    private isReady: boolean;

    connectedCallback() {
        const delegate = this.ready.bind(this);
        this.isReady = LocalizationService.subscribe(delegate);
        if (this.isReady)
            this.ready();
    }

    ready() {
        this.innerHTML = PMForm.template;
        let button = this.querySelector("button");
        button.innerHTML = PopForums.localizations.send;
        button.addEventListener("click", () => {
            let textBox = this.querySelector("textarea") as HTMLTextAreaElement;
            PopForums.currentPmState.send(textBox.value);
            textBox.value = "";
        });
    }

    static template: string = 
`<div class="input-group mb-3">
    <textarea class="form-control"></textarea>
    <button class="btn btn-primary" type="button"></button>
</div>`;
}

customElements.define('pf-pmform', PMForm);

}