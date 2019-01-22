(function(e,t){"object"==typeof exports&&"object"==typeof module?module.exports=t(require("react"),require("react-dom")):"function"==typeof define&&define.amd?define("react-aria-menubutton",["react","react-dom"],t):"object"==typeof exports?exports.ReactAriaMenuButton=t(require("react"),require("react-dom")):e.ReactAriaMenuButton=t(e.React,e.ReactDOM)})(this,function(e,t){return(function(e){function t(r){if(n[r])return n[r].exports;var o=n[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,t),o.l=!0,o.exports}var n={};return t.m=e,t.c=n,t.i=function(e){return e},t.d=function(e,n,r){t.o(e,n)||Object.defineProperty(e,n,{configurable:!1,enumerable:!0,get:r})},t.n=function(e){var n=e&&e.__esModule?function(){return e.default}:function(){return e};return t.d(n,"a",n),n},t.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},t.p="",t(t.s=14)})([function(e,t,n){"use strict";e.exports=function(e,t,n){for(var r in t)t.hasOwnProperty(r)&&(n[r]||(e[r]=t[r]))}},function(e,t,n){if("production"!==JSON.stringify("production")){var r="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103,o=function(e){return"object"==typeof e&&null!==e&&e.$$typeof===r};e.exports=n(18)(o,!0)}else e.exports=n(17)()},function(t,n){t.exports=e},function(e,t,n){"use strict";function r(e){return function(){return e}}var o=function(){};o.thatReturns=r,o.thatReturnsFalse=r(!1),o.thatReturnsTrue=r(!0),o.thatReturnsNull=r(null),o.thatReturnsThis=function(){return this},o.thatReturnsArgument=function(e){return e},e.exports=o},function(e,t,n){"use strict";function r(e,t,n,r,i,a,s,c){if(o(t),!e){var u;if(void 0===t)u=new Error("Minified exception occurred; use the non-minified dev environment for the full error message and additional helpful warnings.");else{var p=[n,r,i,a,s,c],l=0;u=new Error(t.replace(/%s/g,function(){return p[l++]})),u.name="Invariant Violation"}throw u.framesToPop=1,u}}var o=function(e){};"production"!==JSON.stringify("production")&&(o=function(e){if(void 0===e)throw new Error("invariant requires an error message argument")}),e.exports=r},function(e,t,n){"use strict";e.exports="SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED"},function(e,t,n){"use strict";function r(e,t){s[e]=t}function o(e){delete s[e]}function i(e,t){var n=s[e];if(!n)throw new Error("Cannot open "+c);n.openMenu(t)}function a(e,t){var n=s[e];if(!n)throw new Error("Cannot close "+c);n.closeMenu(t)}var s={},c="a menu outside a mounted Wrapper with an id, or a menu that does not exist";e.exports={registerManager:r,unregisterManager:o,openMenu:i,closeMenu:a}},function(e,t,n){"use strict";var r=n(3),o=r;if("production"!==JSON.stringify("production")){var i=function(e){for(var t=arguments.length,n=Array(t>1?t-1:0),r=1;r<t;r++)n[r-1]=arguments[r];var o=0,i="Warning: "+e.replace(/%s/g,function(){return n[o++]});"undefined"!=typeof console&&console.error(i);try{throw new Error(i)}catch(e){}};o=function(e,t){if(void 0===t)throw new Error("`warning(condition, format, ...args)` requires a warning message argument");if(0!==t.indexOf("Failed Composite propType: ")&&!e){for(var n=arguments.length,r=Array(n>2?n-2:0),o=2;o<n;o++)r[o-2]=arguments[o];i.apply(void 0,[t].concat(r))}}}e.exports=o},function(e,n){e.exports=t},function(e,t,n){"use strict";function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function o(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}var a=(function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}})(),s=n(2),c=n(1),u=n(0),p={children:c.node.isRequired,disabled:c.bool,tag:c.string},l=(function(e){function t(){var e,n,i,a;r(this,t);for(var s=arguments.length,c=Array(s),u=0;u<s;u++)c[u]=arguments[u];return n=i=o(this,(e=t.__proto__||Object.getPrototypeOf(t)).call.apply(e,[this].concat(c))),i.handleKeyDown=function(e){if(!i.props.disabled){var t=i.context.ambManager;switch(e.key){case"ArrowDown":e.preventDefault(),t.isOpen?t.focusItem(0):t.openMenu();break;case"Enter":case" ":e.preventDefault(),t.toggleMenu();break;case"Escape":t.handleMenuKey(e);break;default:t.handleButtonNonArrowKey(e)}}},i.handleClick=function(){i.props.disabled||i.context.ambManager.toggleMenu({},{focusMenu:!1})},a=n,o(i,a)}return i(t,e),a(t,[{key:"componentWillMount",value:function(){this.context.ambManager.button=this}},{key:"componentWillUnmount",value:function(){this.context.ambManager.destroy()}},{key:"render",value:function(){var e=this.props,t={role:"button",tabIndex:e.disabled?"":"0","aria-haspopup":!0,"aria-expanded":this.context.ambManager.isOpen,"aria-disabled":e.disabled,onKeyDown:this.handleKeyDown,onClick:this.handleClick,onBlur:this.context.ambManager.handleBlur};return u(t,e,p),s.createElement(e.tag,t,e.children)}}]),t})(s.Component);l.propTypes=p,l.contextTypes={ambManager:c.object.isRequired},l.defaultProps={tag:"span"},e.exports=l},function(e,t,n){"use strict";function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function o(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}var a,s,c=(function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}})(),u=n(2),p=n(8),l=n(1),f=n(19),d=n(0),m={children:l.oneOfType([l.func,l.node]).isRequired,tag:l.string};e.exports=(s=a=(function(e){function t(){var e,n,i,a;r(this,t);for(var s=arguments.length,c=Array(s),u=0;u<s;u++)c[u]=arguments[u];return n=i=o(this,(e=t.__proto__||Object.getPrototypeOf(t)).call.apply(e,[this].concat(c))),i.addTapListener=function(){var e=p.findDOMNode(i);if(e){var t=e.ownerDocument;t&&(i.tapListener=f(t.documentElement,i.handleTap))}},i.handleTap=function(e){p.findDOMNode(i).contains(e.target)||p.findDOMNode(i.context.ambManager.button).contains(e.target)||i.context.ambManager.closeMenu()},a=n,o(i,a)}return i(t,e),c(t,[{key:"componentWillMount",value:function(){this.context.ambManager.menu=this}},{key:"componentDidUpdate",value:function(){var e=this.context.ambManager;e.isOpen&&!this.tapListener?this.addTapListener():!e.isOpen&&this.tapListener&&(this.tapListener.remove(),delete this.tapListener),e.isOpen||e.clearItems()}},{key:"componentWillUnmount",value:function(){this.tapListener&&this.tapListener.remove(),this.context.ambManager.destroy()}},{key:"render",value:function(){var e=this.props,t=this.context.ambManager,n=(function(){return"function"==typeof e.children?e.children({isOpen:t.isOpen}):!!t.isOpen&&e.children})();if(!n)return!1;var r={onKeyDown:t.handleMenuKey,role:"menu",onBlur:t.handleBlur,tabIndex:-1};return d(r,e,m),u.createElement(e.tag,r,n)}}]),t})(u.Component),a.propTypes=m,a.defaultProps={tag:"div"},a.contextTypes={ambManager:l.object.isRequired},s)},function(e,t,n){"use strict";function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function o(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}var a=(function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}})(),s=n(2),c=n(1),u=n(0),p={children:c.node.isRequired,tag:c.string,text:c.string,value:c.any},l=(function(e){function t(){var e,n,i,a;r(this,t);for(var s=arguments.length,c=Array(s),u=0;u<s;u++)c[u]=arguments[u];return n=i=o(this,(e=t.__proto__||Object.getPrototypeOf(t)).call.apply(e,[this].concat(c))),i.handleKeyDown=function(e){"Enter"!==e.key&&" "!==e.key||(e.preventDefault(),i.selectItem(e))},i.selectItem=function(e){var t=void 0!==i.props.value?i.props.value:i.props.children;i.context.ambManager.handleSelection(t,e)},i.registerNode=function(e){i.node=e},a=n,o(i,a)}return i(t,e),a(t,[{key:"componentDidMount",value:function(){this.context.ambManager.addItem({node:this.node,text:this.props.text})}},{key:"render",value:function(){var e={onClick:this.selectItem,onKeyDown:this.handleKeyDown,role:"menuitem",tabIndex:"-1",ref:this.registerNode};return u(e,this.props,p),s.createElement(this.props.tag,e,this.props.children)}}]),t})(s.Component);l.propTypes=p,l.defaultProps={tag:"div"},l.contextTypes={ambManager:c.object.isRequired},e.exports=l},function(e,t,n){"use strict";function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function o(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}var a=(function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}})(),s=n(2),c=n(1),u=n(13),p=n(0),l={children:c.node.isRequired,onMenuToggle:c.func,onSelection:c.func.isRequired,closeOnSelection:c.bool,tag:c.string},f=(function(e){function t(){return r(this,t),o(this,(t.__proto__||Object.getPrototypeOf(t)).apply(this,arguments))}return i(t,e),a(t,[{key:"getChildContext",value:function(){return{ambManager:this.manager}}},{key:"componentWillMount",value:function(){this.manager=u({onMenuToggle:this.props.onMenuToggle,onSelection:this.props.onSelection,closeOnSelection:this.props.closeOnSelection,id:this.props.id})}},{key:"render",value:function(){var e={};return p(e,this.props,l),s.createElement(this.props.tag,e,this.props.children)}}]),t})(s.Component);f.propTypes=l,f.defaultProps={tag:"div"},f.childContextTypes={ambManager:c.object},e.exports=f},function(e,t,n){"use strict";function r(){var e=this;e.blurTimer=setTimeout(function(){var t=a.findDOMNode(e.button);if(t){var n=t.ownerDocument.activeElement;if(!t||n!==t){var r=a.findDOMNode(e.menu);if(r===n)return void e.focusItem(0);r&&r.contains(n)||e.isOpen&&e.closeMenu({focusButton:!1})}}},0)}function o(e,t){this.options.closeOnSelection&&this.closeMenu({focusButton:!0}),this.options.onSelection(e,t)}function i(e){this.isOpen&&"Escape"===e.key&&(e.preventDefault(),this.closeMenu({focusButton:!0}))}var a=n(8),s=n(15),c=n(6),u={wrap:!0,stringSearch:!0},p={init:function(e){this.options=e||{},void 0===this.options.closeOnSelection&&(this.options.closeOnSelection=!0),this.options.id&&c.registerManager(this.options.id,this),this.handleBlur=r.bind(this),this.handleSelection=o.bind(this),this.handleMenuKey=i.bind(this),this.focusGroup=s(u),this.button=null,this.menu=null,this.isOpen=!1},focusItem:function(e){this.focusGroup.focusNodeAtIndex(e)},addItem:function(e){this.focusGroup.addMember(e)},clearItems:function(){this.focusGroup.clearMembers()},handleButtonNonArrowKey:function(e){this.focusGroup._handleUnboundKey(e)},destroy:function(){this.button=null,this.menu=null,this.focusGroup.deactivate(),clearTimeout(this.blurTimer),clearTimeout(this.moveFocusTimer)},update:function(){this.menu.setState({isOpen:this.isOpen}),this.button.setState({menuOpen:this.isOpen}),this.options.onMenuToggle&&this.options.onMenuToggle({isOpen:this.isOpen})},openMenu:function(e){if(!this.isOpen&&(e=e||{},void 0===e.focusMenu&&(e.focusMenu=!0),this.isOpen=!0,this.update(),this.focusGroup.activate(),e.focusMenu)){var t=this;this.moveFocusTimer=setTimeout(function(){t.focusItem(0)},0)}},closeMenu:function(e){this.isOpen&&(e=e||{},this.isOpen=!1,this.update(),e.focusButton&&a.findDOMNode(this.button).focus())},toggleMenu:function(e,t){e=e||{},t=t||{},this.isOpen?this.closeMenu(e):this.openMenu(t)}};e.exports=function(e){var t=Object.create(p);return t.init(e),t}},function(e,t,n){"use strict";var r=n(6);e.exports={Wrapper:n(12),Button:n(9),Menu:n(10),MenuItem:n(11),openMenu:r.openMenu,closeMenu:r.closeMenu}},function(e,t){function n(e){e=e||{};var t=e.keybindings||{};this._settings={keybindings:{next:t.next||{keyCode:40},prev:t.prev||{keyCode:38},first:t.first,last:t.last},wrap:e.wrap,stringSearch:e.stringSearch,stringSearchDelay:800},this._keybindingsLookup=[];var n,r;for(n in this._settings.keybindings)r=this._settings.keybindings[n],r&&[].concat(r).forEach(function(e){e.metaKey=e.metaKey||!1,e.ctrlKey=e.ctrlKey||!1,e.altKey=e.altKey||!1,e.shiftKey=e.shiftKey||!1,this._keybindingsLookup.push({action:n,eventMatcher:e})}.bind(this));this._searchString="",this._members=[],e.members&&this.setMembers(e.members),this._boundHandleKeydownEvent=this._handleKeydownEvent.bind(this)}function r(e,t){for(var n in e)if(void 0!==t[n]&&e[n]!==t[n])return!1;return!0}function o(e){return e>=65&&e<=90}function i(e){e&&e.focus&&(e.focus(),"input"===e.tagName.toLowerCase()&&e.select())}n.prototype.activate=function(){return document.addEventListener("keydown",this._boundHandleKeydownEvent,!0),this},n.prototype.deactivate=function(){return document.removeEventListener("keydown",this._boundHandleKeydownEvent,!0),this._clearSearchStringRefreshTimer(),this},n.prototype._handleKeydownEvent=function(e){if(this._getActiveElementIndex()!==-1){var t=!1;this._keybindingsLookup.forEach(function(n){if(r(n.eventMatcher,e))switch(t=!0,e.preventDefault(),n.action){case"next":this.moveFocusForward();break;case"prev":this.moveFocusBack();break;case"first":this.moveFocusToFirst();break;case"last":this.moveFocusToLast();break;default:return}}.bind(this)),t||this._handleUnboundKey(e)}},n.prototype.moveFocusForward=function(){var e,t=this._getActiveElementIndex();return e=t<this._members.length-1?t+1:this._settings.wrap?0:t,this.focusNodeAtIndex(e),e},n.prototype.moveFocusBack=function(){var e,t=this._getActiveElementIndex();return e=t>0?t-1:this._settings.wrap?this._members.length-1:t,this.focusNodeAtIndex(e),e},n.prototype.moveFocusToFirst=function(){this.focusNodeAtIndex(0)},n.prototype.moveFocusToLast=function(){this.focusNodeAtIndex(this._members.length-1)},n.prototype._handleUnboundKey=function(e){if(this._settings.stringSearch){if(""!==this._searchString&&(" "===e.key||32===e.keyCode))return e.preventDefault(),-1;if(!o(e.keyCode))return-1;if(e.ctrlKey||e.metaKey||e.altKey)return-1;e.preventDefault(),this._addToSearchString(String.fromCharCode(e.keyCode)),this._runStringSearch()}},n.prototype._clearSearchString=function(){this._searchString=""},n.prototype._addToSearchString=function(e){this._searchString+=e.toLowerCase()},n.prototype._startSearchStringRefreshTimer=function(){var e=this;this._clearSearchStringRefreshTimer(),this._stringSearchTimer=setTimeout(function(){e._clearSearchString()},this._settings.stringSearchDelay)},n.prototype._clearSearchStringRefreshTimer=function(){clearTimeout(this._stringSearchTimer)},n.prototype._runStringSearch=function(){this._startSearchStringRefreshTimer(),this.moveFocusByString(this._searchString)},n.prototype.moveFocusByString=function(e){for(var t,n=0,r=this._members.length;n<r;n++)if(t=this._members[n],t.text&&0===t.text.indexOf(e))return i(t.node)},n.prototype._findIndexOfNode=function(e){for(var t=0,n=this._members.length;t<n;t++)if(this._members[t].node===e)return t;return-1},n.prototype._getActiveElementIndex=function(){return this._findIndexOfNode(document.activeElement)},n.prototype.focusNodeAtIndex=function(e){var t=this._members[e];return t&&i(t.node),this},n.prototype.addMember=function(e,t){var n=e.node||e,r=e.text||n.getAttribute("data-focus-group-text")||n.textContent||"";this._checkNode(n);var o=r.replace(/[\W_]/g,"").toLowerCase(),i={node:n,text:o};return null!==t&&void 0!==t?this._members.splice(t,0,i):this._members.push(i),this},n.prototype.removeMember=function(e){var t="number"==typeof e?e:this._findIndexOfNode(e);if(t!==-1)return this._members.splice(t,1),this},n.prototype.clearMembers=function(){return this._members=[],this},n.prototype.setMembers=function(e){this.clearMembers();for(var t=0,n=e.length;t<n;t++)this.addMember(e[t]);return this},n.prototype.getMembers=function(){return this._members},n.prototype._checkNode=function(e){if(!e.nodeType||e.nodeType!==window.Node.ELEMENT_NODE)throw new Error("focus-group: only DOM nodes allowed");return e},e.exports=function(e){return new n(e)}},function(e,t,n){"use strict";function r(e,t,n,r,c){if("production"!==JSON.stringify("production"))for(var u in e)if(e.hasOwnProperty(u)){var p;try{o("function"==typeof e[u],"%s: %s type `%s` is invalid; it must be a function, usually from React.PropTypes.",r||"React class",n,u),p=e[u](t,u,r,n,null,a)}catch(e){p=e}if(i(!p||p instanceof Error,"%s: type specification of %s `%s` is invalid; the type checker function must return `null` or an `Error` but returned a %s. You may have forgotten to pass an argument to the type checker creator (arrayOf, instanceOf, objectOf, oneOf, oneOfType, and shape all require an argument).",r||"React class",n,u,typeof p),p instanceof Error&&!(p.message in s)){s[p.message]=!0;var l=c?c():"";i(!1,"Failed %s type: %s%s",n,p.message,null!=l?l:"")}}}if("production"!==JSON.stringify("production"))var o=n(4),i=n(7),a=n(5),s={};e.exports=r},function(e,t,n){"use strict";var r=n(3),o=n(4),i=n(5);e.exports=function(){function e(e,t,n,r,a,s){s!==i&&o(!1,"Calling PropTypes validators directly is not supported by the `prop-types` package. Use PropTypes.checkPropTypes() to call them. Read more at http://fb.me/use-check-prop-types")}function t(){return e}e.isRequired=e;var n={array:e,bool:e,func:e,number:e,object:e,string:e,symbol:e,any:e,arrayOf:t,element:e,instanceOf:t,node:e,objectOf:t,oneOf:t,oneOfType:t,shape:t};return n.checkPropTypes=r,n.PropTypes=n,n}},function(e,t,n){"use strict";var r=n(3),o=n(4),i=n(7),a=n(5),s=n(16);e.exports=function(e,t){function n(e){var t=e&&(N&&e[N]||e["@@iterator"]);if("function"==typeof t)return t}function c(e,t){return e===t?0!==e||1/e===1/t:e!==e&&t!==t}function u(e){this.message=e,this.stack=""}function p(e){function n(n,c,p,l,f,d,m){if(l=l||"<<anonymous>>",d=d||p,m!==a)if(t)o(!1,"Calling PropTypes validators directly is not supported by the `prop-types` package. Use `PropTypes.checkPropTypes()` to call them. Read more at http://fb.me/use-check-prop-types");else if("production"!==JSON.stringify("production")&&"undefined"!=typeof console){var h=l+":"+p;!r[h]&&s<3&&(i(!1,"You are manually calling a React.PropTypes validation function for the `%s` prop on `%s`. This is deprecated and will throw in the standalone `prop-types` package. You may be seeing this warning due to a third-party PropTypes library. See https://fb.me/react-warning-dont-call-proptypes for details.",d,l),r[h]=!0,s++)}return null==c[p]?n?new u(null===c[p]?"The "+f+" `"+d+"` is marked as required in `"+l+"`, but its value is `null`.":"The "+f+" `"+d+"` is marked as required in `"+l+"`, but its value is `undefined`."):null:e(c,p,l,f,d)}if("production"!==JSON.stringify("production"))var r={},s=0;var c=n.bind(null,!1);return c.isRequired=n.bind(null,!0),c}function l(e){function t(t,n,r,o,i,a){var s=t[n];if(S(s)!==e)return new u("Invalid "+o+" `"+i+"` of type `"+w(s)+"` supplied to `"+r+"`, expected `"+e+"`.");return null}return p(t)}function f(){return p(r.thatReturnsNull)}function d(e){function t(t,n,r,o,i){if("function"!=typeof e)return new u("Property `"+i+"` of component `"+r+"` has invalid PropType notation inside arrayOf.");var s=t[n];if(!Array.isArray(s)){return new u("Invalid "+o+" `"+i+"` of type `"+S(s)+"` supplied to `"+r+"`, expected an array.")}for(var c=0;c<s.length;c++){var p=e(s,c,r,o,i+"["+c+"]",a);if(p instanceof Error)return p}return null}return p(t)}function m(){function t(t,n,r,o,i){var a=t[n];if(!e(a)){return new u("Invalid "+o+" `"+i+"` of type `"+S(a)+"` supplied to `"+r+"`, expected a single ReactElement.")}return null}return p(t)}function h(e){function t(t,n,r,o,i){if(!(t[n]instanceof e)){var a=e.name||"<<anonymous>>";return new u("Invalid "+o+" `"+i+"` of type `"+k(t[n])+"` supplied to `"+r+"`, expected instance of `"+a+"`.")}return null}return p(t)}function v(e){function t(t,n,r,o,i){for(var a=t[n],s=0;s<e.length;s++)if(c(a,e[s]))return null;return new u("Invalid "+o+" `"+i+"` of value `"+a+"` supplied to `"+r+"`, expected one of "+JSON.stringify(e)+".")}return Array.isArray(e)?p(t):("production"!==JSON.stringify("production")&&i(!1,"Invalid argument supplied to oneOf, expected an instance of array."),r.thatReturnsNull)}function b(e){function t(t,n,r,o,i){if("function"!=typeof e)return new u("Property `"+i+"` of component `"+r+"` has invalid PropType notation inside objectOf.");var s=t[n],c=S(s);if("object"!==c)return new u("Invalid "+o+" `"+i+"` of type `"+c+"` supplied to `"+r+"`, expected an object.");for(var p in s)if(s.hasOwnProperty(p)){var l=e(s,p,r,o,i+"."+p,a);if(l instanceof Error)return l}return null}return p(t)}function _(e){function t(t,n,r,o,i){for(var s=0;s<e.length;s++){if(null==(0,e[s])(t,n,r,o,i,a))return null}return new u("Invalid "+o+" `"+i+"` supplied to `"+r+"`.")}if(!Array.isArray(e))return"production"!==JSON.stringify("production")&&i(!1,"Invalid argument supplied to oneOfType, expected an instance of array."),r.thatReturnsNull;for(var n=0;n<e.length;n++){var o=e[n];if("function"!=typeof o)return i(!1,"Invalid argument supplid to oneOfType. Expected an array of check functions, but received %s at index %s.",M(o),n),r.thatReturnsNull}return p(t)}function y(){function e(e,t,n,r,o){return x(e[t])?null:new u("Invalid "+r+" `"+o+"` supplied to `"+n+"`, expected a ReactNode.")}return p(e)}function g(e){function t(t,n,r,o,i){var s=t[n],c=S(s);if("object"!==c)return new u("Invalid "+o+" `"+i+"` of type `"+c+"` supplied to `"+r+"`, expected `object`.");for(var p in e){var l=e[p];if(l){var f=l(s,p,r,o,i+"."+p,a);if(f)return f}}return null}return p(t)}function x(t){switch(typeof t){case"number":case"string":case"undefined":return!0;case"boolean":return!t;case"object":if(Array.isArray(t))return t.every(x);if(null===t||e(t))return!0;var r=n(t);if(!r)return!1;var o,i=r.call(t);if(r!==t.entries){for(;!(o=i.next()).done;)if(!x(o.value))return!1}else for(;!(o=i.next()).done;){var a=o.value;if(a&&!x(a[1]))return!1}return!0;default:return!1}}function O(e,t){return"symbol"===e||("Symbol"===t["@@toStringTag"]||"function"==typeof Symbol&&t instanceof Symbol)}function S(e){var t=typeof e;return Array.isArray(e)?"array":e instanceof RegExp?"object":O(t,e)?"symbol":t}function w(e){if(void 0===e||null===e)return""+e;var t=S(e);if("object"===t){if(e instanceof Date)return"date";if(e instanceof RegExp)return"regexp"}return t}function M(e){var t=w(e);switch(t){case"array":case"object":return"an "+t;case"boolean":case"date":case"regexp":return"a "+t;default:return t}}function k(e){return e.constructor&&e.constructor.name?e.constructor.name:"<<anonymous>>"}var N="function"==typeof Symbol&&Symbol.iterator,E={array:l("array"),bool:l("boolean"),func:l("function"),number:l("number"),object:l("object"),string:l("string"),symbol:l("symbol"),any:f(),arrayOf:d,element:m(),instanceOf:h,node:y(),objectOf:b,oneOf:v,oneOfType:_,shape:g};return u.prototype=Error.prototype,E.checkPropTypes=s,E.PropTypes=E,E}},function(e,t){e.exports=function(e,t,n){function r(e){m||t(e)}function o(t){m=!0,f||(f=!0,e.addEventListener("touchmove",i,n),e.addEventListener("touchend",a,n),e.addEventListener("touchcancel",s,n),d=!1,p=t.touches[0].clientX,l=t.touches[0].clientY)}function i(e){d||Math.abs(e.touches[0].clientX-p)<=10&&Math.abs(e.touches[0].clientY-l)<=10||(d=!0)}function a(e){f=!1,c(),d||t(e)}function s(){f=!1,d=!1,p=0,l=0}function c(){e.removeEventListener("touchmove",i,n),e.removeEventListener("touchend",a,n),e.removeEventListener("touchcancel",s,n)}function u(){e.removeEventListener("click",r,n),e.removeEventListener("touchstart",o,n),c()}var p=0,l=0,f=!1,d=!1,m=!1;return e.addEventListener("click",r,n),e.addEventListener("touchstart",o,n),{remove:u}}}])}),define("spectrum/button/button",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.Button=function(e){function t(){n&&n.focus(),b&&b.apply(void 0,arguments)}var n,a=e.children,s=e.className,c=void 0===s?"":s,u=e.disabled,p=e.href,l=e.size,f=void 0===l?"default":l,d=e.variant,m=void 0===d?"primary":d,h=e.fullWidth,v=void 0!==h&&h,b=e.onClick,_=e.shouldWrapContent,y=void 0!==_&&_,g=e.type,x=r.__rest(e,["children","className","disabled","href","size","variant","fullWidth","onClick","shouldWrapContent","type"]),O=x.tagName,S=void 0===O?"button":O,w=r.__rest(x,["tagName"]),M=o.default(c,{"mc-button":"styleless"!==m,"mc-button-styleless":"styleless"===m,"mc-button-primary":"primary"===m,"mc-button-secondary":"secondary"===m||"borderless"===m,"mc-button-circular":"circular"===m,"mc-button-invisible":"invisible"===m,"mc-button-borderless":"borderless"===m,"mc-button-large":"large"===f,"mc-button-small":"small"===f,"mc-button-disabled":u,"mc-button-full-width":v,"mc-button-wrap-content":y}),k=i.createElement("span",{className:"mc-button-content"},a),N=r.__assign({},w,{className:M,onClick:t});return p&&(S="a"),"button"===S?(N.disabled=u,N.type=g):("a"===S&&(N.href=p),u&&(N.tabIndex=-1)),i.createElement(S,r.__assign({},N,{ref:function(e){return n=e}}),k)},t.Button.displayName="Button"}),define("spectrum/button",["require","exports","tslib","spectrum/button/button"],function(e,t,n,r){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),n.__exportStar(r,t)}),define("spectrum/checkbox/checkbox",["require","exports","tslib","tslib","classnames","spectrum/util/debounce","react"],function(e,t,n,r,o,i,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),a=n.__importStar(a);var s=(function(e){function t(t){var n=e.call(this,t)||this;return n.triggerChange=function(e,t,r){!n.props.disabled&&n.props.onChange&&n.props.onChange(e,t,r)},n.handleChange=function(e){var t=e.currentTarget.checked?"checked":"unchecked";"mixed"===n.props.checked&&(t="unchecked"),n.debouncedTriggerChange(t,{shiftKey:!1},{target:e.currentTarget})},n.handleClick=function(e){var t="unchecked";"unchecked"===n.props.checked&&(t="checked"),n.input&&n.input.focus(),n.debouncedTriggerChange(t,{shiftKey:e.shiftKey},{target:n.input})},n.debouncedTriggerChange=i.debounce(n.triggerChange,0,!0),n}return r.__extends(t,e),t.prototype.render=function(){var e=this,t=this.props,n=t.ariaLabel,r=void 0===n?"":n,i=t.checked,s=void 0===i?"unchecked":i,p=t.className,l=void 0===p?"":p,f=t.disabled,d=t.id,m=void 0===d?"":d,h=t.isHovered,v=t.name,b=t.tabIndex,_=t.variant,y=void 0===_?"default":_,g=t.autoFocus,x=void 0!==g&&g,O=o.default(l,{"mc-checkbox":!0,"mc-checkbox-checked":"checked"===s,"mc-checkbox-disabled":f,"mc-checkbox-hovered":h,"mc-checkbox-invisible":"invisible"===y,"mc-checkbox-mixed":"mixed"===s,"mc-checkbox-unchecked":"unchecked"===s}),S="mixed"===s?"mixed":"checked"===s;return a.createElement("label",{className:O,onClick:this.handleClick},a.createElement("input",{"aria-checked":S,"aria-label":r,checked:"checked"===s,className:"mc-checkbox-input",disabled:f,id:m,name:v,onChange:this.handleChange,ref:function(t){e.input=t,t&&(t.indeterminate="mixed"===s)},tabIndex:b,type:"checkbox",autoFocus:x}),a.createElement("span",{className:"mc-checkbox-border"}),c(),u())},t})(a.PureComponent);t.Checkbox=s;var c=function(){return a.createElement("svg",{"aria-hidden":"true",width:"24px",height:"24px",viewBox:"0 0 24 24",version:"1.1",className:"mc-checkbox-checked-icon"},a.createElement("path",{fill:"#ffffff",d:"M13.1239594,15.613961 L9.12395935,15.613961 L9.12395935,17.613961 L14.1239594,17.613961 L15.1239594,17.613961 L15.1239594,5.61396103 L13.1239594,5.61396103 L13.1239594,15.613961 Z",transform:"translate(12.123959, 11.613961) rotate(-315.000000) translate(-12.123959, -11.613961)"}))},u=function(){return a.createElement("svg",{"aria-hidden":"true",className:"mc-checkbox-mixed-icon",width:"6px",height:"2px"},a.createElement("rect",{id:"Rectangle",x:"0",y:"0",width:"6",height:"2",fill:"#C1C7CD"}))}}),define("spectrum/checkbox",["require","exports","tslib","spectrum/checkbox/checkbox"],function(e,t,n,r){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),n.__exportStar(r,t)}),define("spectrum/fixed_aria_menu_button",["require","exports","tslib","tslib","react","react-dom","react-aria-menubutton"],function(e,t,n,r,o,i,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importStar(o),i=n.__importStar(i),t.FixedAriaMenuButton=function(e,t){function n(){u.disabled||(s&&i.findDOMNode(s).focus(),c.toggleMenu({},{focusMenu:!1}),p&&p.apply(this,arguments))}var s,c=t.ambManager,u=(e.ref,r.__rest(e,["ref"])),p=u.onMouseDown;return o.createElement(a.Button,r.__assign({ref:function(e){return s=e},onClick:n},u))},t.FixedAriaMenuButton.contextTypes={ambManager:function(){return null}},t.FixedAriaMenuButton.displayName="FixedAriaMenuButton"}),define("spectrum/patched_aria_menu_item",["require","exports","tslib","tslib","react-aria-menubutton"],function(e,t,n,r,o){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r);var i=(function(e){function t(){return null!==e&&e.apply(this,arguments)||this}return r.__extends(t,e),t.prototype.componentDidMount=function(){this.props.disabled||o.MenuItem.prototype.componentDidMount.apply(this,arguments)},t})(o.MenuItem);t.PatchedAriaMenuItem=i}),define("spectrum/popover",["require","exports","tslib","spectrum/popover/popover","spectrum/popover/popover_content","spectrum/popover/popover_content_description","spectrum/popover/popover_content_item","spectrum/popover/popover_content_separator","spectrum/popover/popover_content_title","spectrum/popover/popover_item_group","spectrum/popover/popover_item_group_separator","spectrum/popover/popover_item_group_title","spectrum/popover/popover_selectable_item","spectrum/popover/popover_trigger"],function(e,t,n,r,o,i,a,s,c,u,p,l,f,d){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),n.__exportStar(r,t),n.__exportStar(o,t),n.__exportStar(i,t),n.__exportStar(a,t),n.__exportStar(s,t),n.__exportStar(c,t),n.__exportStar(u,t),n.__exportStar(p,t),n.__exportStar(l,t),n.__exportStar(f,t),n.__exportStar(d,t)}),define("spectrum/popover/popover",["require","exports","tslib","tslib","classnames","react","react-aria-menubutton"],function(e,t,n,r,o,i,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),
r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.Popover=function(e){var t=e.className,n=e.onSelection,s=e.ref,c=r.__rest(e,["className","onSelection","ref"]),u=o.default(t,"mc-popover");return i.createElement(a.Wrapper,r.__assign({className:u,onSelection:n||function(){},ref:s},c))},t.Popover.displayName="Popover"}),define("spectrum/popover/popover_content",["require","exports","tslib","tslib","classnames","react","react-aria-menubutton"],function(e,t,n,r,o,i,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverContent=function(e){var t=e.attachment,n=void 0===t?"left":t,s=e.position,c=void 0===s?"below":s,u=e.children,p=e.className,l=e.height,f=e.isRevealed,d=e.ref,m=e.tagName,h=void 0===m?"nav":m,v=e.width,b=r.__rest(e,["attachment","position","children","className","height","isRevealed","ref","tagName","width"]),_=o.default(p,{"mc-popover-content":!0,"mc-popover-content-attach-left":"left"===n,"mc-popover-content-attach-right":"right"===n,"mc-popover-content-position-above":"above"===c,"mc-popover-content-position-below":"below"===c,"mc-popover-content-position-right":"right"===c,"mc-popover-content-position-left":"left"===c}),y={width:v,height:l},g=i.createElement("div",{className:"mc-popover-content-scroller",style:y},u);return i.createElement("div",{className:_},i.createElement(a.Menu,r.__assign({tag:h,className:"mc-popover-content-menu",ref:d},b),function(e){var t=e.isOpen;return("boolean"==typeof f?f:t)&&g}))},t.PopoverContent.displayName="PopoverContent"}),define("spectrum/popover/popover_content_description",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverContentDescription=function(e){var t=e.className,n=r.__rest(e,["className"]),a=o.default(t,"mc-popover-content-description");return i.createElement("div",r.__assign({className:a},n))},t.PopoverContentDescription.displayName="PopoverContentDescription"}),define("spectrum/popover/popover_content_item",["require","exports","tslib","tslib","classnames","react","spectrum/patched_aria_menu_item"],function(e,t,n,r,o,i,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverContentItem=function(e){var t=e.href,n=e.className,s=e.ref,c=r.__rest(e,["href","className","ref"]),u=c.tagName,p=void 0===u?t&&!c.disabled?"a":"button":u,l=r.__rest(c,["tagName"]),f=o.default(n,"mc-popover-content-item");return i.createElement(a.PatchedAriaMenuItem,r.__assign({tag:p,className:f,ref:s,href:t},l))},t.PopoverContentItem.displayName="PopoverContentItem"}),define("spectrum/popover/popover_content_separator",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverContentSeparator=function(e){var t=e.className,n=r.__rest(e,["className"]),a=o.default(t,"mc-popover-content-separator");return i.createElement("hr",r.__assign({className:a},n))},t.PopoverContentSeparator.displayName="PopoverContentSeparator"}),define("spectrum/popover/popover_content_title",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverContentTitle=function(e){var t=e.className,n=r.__rest(e,["className"]),a=o.default(t,"mc-popover-content-title");return i.createElement("h2",r.__assign({className:a},n))},t.PopoverContentTitle.displayName="PopoverContentTitle"}),define("spectrum/popover/popover_item_group",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverItemGroup=function(e){var t=e.className,n=r.__rest(e,["className"]),a=o.default("mc-popover-item-group",t);return i.createElement("div",r.__assign({className:a},n))},t.PopoverItemGroup.displayName="PopoverItemGroup"}),define("spectrum/popover/popover_item_group_separator",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverItemGroupSeparator=function(e){var t=e.className,n=r.__rest(e,["className"]),a=o.default(t,"mc-popover-item-group-separator");return i.createElement("hr",r.__assign({className:a},n))},t.PopoverItemGroupSeparator.displayName="PopoverItemGroupSeparator"}),define("spectrum/popover/popover_item_group_title",["require","exports","tslib","tslib","classnames","react"],function(e,t,n,r,o,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverItemGroupTitle=function(e){var t=e.className,n=r.__rest(e,["className"]),a=o.default("mc-popover-item-group-title",t);return i.createElement("h2",r.__assign({className:a},n))},t.PopoverItemGroupTitle.displayName="PopoverItemGroupTitle"}),define("spectrum/popover/popover_selectable_item",["require","exports","tslib","tslib","classnames","react","react-aria-menubutton","spectrum/checkbox"],function(e,t,n,r,o,i,a,s){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.BlueCheckIcon=function(){return i.createElement("svg",{width:"24",height:"24",viewBox:"0 0 24 24","aria-hidden":"true"},i.createElement("path",{d:"M10.003 15.15L7.174 12.32 5.76 13.735l3.536 3.536.707.707 8.485-8.485-1.414-1.415-7.071 7.071z",fill:"#0070E0",fillRule:"evenodd"}))},t.PopoverSelectableItem=function(e){var n=e.children,c=e.className,u=e.disabled,p=e.icon,l=e.ref,f=e.selected,d=e.tagName,m=void 0===d?"button":d,h=e.variant,v=void 0===h?"single":h,b=r.__rest(e,["children","className","disabled","icon","ref","selected","tagName","variant"]),_="multiple"===v,y=o.default(c,"mc-popover-selectable-item",{"mc-popover-selectable-item-disabled":u,"mc-popover-selectable-item-selected":f,"mc-popover-multiple-selection-item":_}),g=function(){return _?i.createElement(s.Checkbox,{className:"mc-popover-selectable-item-checkbox",disabled:u,checked:f?"checked":"unchecked"}):f&&i.createElement(t.BlueCheckIcon,null)};return i.createElement(a.MenuItem,r.__assign({tag:m,className:y,disabled:u,ref:l},b),i.createElement("span",{className:"mc-popover-selectable-item-content"},i.createElement("span",{className:"mc-popover-selectable-item-check-wrapper"},g()),i.createElement("span",{className:"mc-popover-selectable-item-text"},n),p&&i.createElement("span",{className:"mc-popover-selectable-item-icon-wrapper"},p)))},t.PopoverSelectableItem.displayName="PopoverSelectableItem"}),define("spectrum/popover/popover_trigger",["require","exports","tslib","tslib","classnames","react","spectrum/fixed_aria_menu_button"],function(e,t,n,r,o,i,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=n.__importStar(r),o=n.__importDefault(o),i=n.__importStar(i),t.PopoverTrigger=function(e){var t=e.className,n=(e.ref,r.__rest(e,["className","ref"])),s=o.default(t,"mc-popover-trigger");return i.createElement(a.FixedAriaMenuButton,r.__assign({tag:"button",className:s},n))},t.PopoverTrigger.displayName="PopoverTrigger"}),define("spectrum/util/debounce",["require","exports"],function(e,t){"use strict";function n(e,t,n){var r=this;void 0===t&&(t=0),void 0===n&&(n=!1);var o=null;return function(){for(var i=[],a=0;a<arguments.length;a++)i[a]=arguments[a];var s=r;o?clearTimeout(o):n&&e.apply(s,i),o=window.setTimeout(function(){o=null,n||e.apply(s,i)},t)}}Object.defineProperty(t,"__esModule",{value:!0}),t.debounce=n});
//# sourceMappingURL=pkg-mcl-base.min.js-vflRNZOci.map