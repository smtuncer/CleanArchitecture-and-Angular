import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  setClassMetadata,
  ɵɵdefineDirective,
  ɵɵdirectiveInject,
  ɵɵlistener
} from "./chunk-RK3CHORZ.js";
import "./chunk-WDMUDEB6.js";

// node_modules/form-validate-angular/fesm2022/form-validate-angular.mjs
var _FormValidateDirective = class _FormValidateDirective {
  constructor(el) {
    this.el = el;
    this.autoValidateMessage = false;
  }
  handleInputEvent(event) {
    const target = event.target;
    this.checkValidation(target);
  }
  handleSubmitEvent(event) {
    this.checkValidation();
  }
  checkValidation(target) {
    if (target) {
      this.validateElement(target);
    } else {
      for (let i = 0; i < this.el.nativeElement.elements.length; i++) {
        const childElement = this.el.nativeElement.elements[i];
        this.validateElement(childElement);
      }
    }
  }
  validateElement(element) {
    if (element.validity !== void 0) {
      const elName = `[name=${element.name}] + div`;
      let divEl;
      if (element.name !== "") {
        divEl = document.querySelector(elName);
      }
      if (!element.validity.valid) {
        if (this.autoValidateMessage && divEl !== null) {
          divEl.innerHTML = element.validationMessage;
        }
        element.classList.add("is-invalid");
      } else {
        element.classList.remove("is-invalid");
      }
    }
  }
};
_FormValidateDirective.ɵfac = function FormValidateDirective_Factory(__ngFactoryType__) {
  return new (__ngFactoryType__ || _FormValidateDirective)(ɵɵdirectiveInject(ElementRef));
};
_FormValidateDirective.ɵdir = ɵɵdefineDirective({
  type: _FormValidateDirective,
  selectors: [["", "formValidate", ""]],
  hostBindings: function FormValidateDirective_HostBindings(rf, ctx) {
    if (rf & 1) {
      ɵɵlistener("keyup", function FormValidateDirective_keyup_HostBindingHandler($event) {
        return ctx.handleInputEvent($event);
      })("change", function FormValidateDirective_change_HostBindingHandler($event) {
        return ctx.handleInputEvent($event);
      })("submit", function FormValidateDirective_submit_HostBindingHandler($event) {
        return ctx.handleSubmitEvent($event);
      });
    }
  },
  inputs: {
    autoValidateMessage: "autoValidateMessage"
  },
  standalone: true
});
var FormValidateDirective = _FormValidateDirective;
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(FormValidateDirective, [{
    type: Directive,
    args: [{
      selector: "[formValidate]",
      standalone: true
    }]
  }], () => [{
    type: ElementRef
  }], {
    autoValidateMessage: [{
      type: Input,
      args: ["autoValidateMessage"]
    }],
    handleInputEvent: [{
      type: HostListener,
      args: ["keyup", ["$event"]]
    }, {
      type: HostListener,
      args: ["change", ["$event"]]
    }],
    handleSubmitEvent: [{
      type: HostListener,
      args: ["submit", ["$event"]]
    }]
  });
})();
export {
  FormValidateDirective
};
//# sourceMappingURL=form-validate-angular.js.map
