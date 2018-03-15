using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace WizardBase {
    internal class WizardStepCollectionEditor : CollectionEditor {
        public WizardStepCollectionEditor(Type type) : base(type) {
        }

        protected override Type[] CreateNewItemTypes() {
            return new[] { typeof(StartStep), typeof(LicenceStep), typeof(IntermediateStep), typeof(FinishStep) };
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            var steps = (GenericCollection<WizardStep>)value;
            var owner = (WizardControl)steps.Owner;
            var container = (IDesignerHost)context.Container;
            int count = steps.Count;
            object obj2 = base.EditValue(context, provider, value);
            if (steps.Count >= count) {
                return obj2;
            }
            SelectWizard(owner, container);
            return obj2;
        }

        private static void SelectWizard(IComponent wizardControl, IDesignerHost host) {
            if (wizardControl == null) {
                return;
            }
            if (host == null) {
                return;
            }
            while (true) {
                var designer = (WizardDesigner)host.GetDesigner(wizardControl);
                if (designer == null) {
                    return;
                }
                var service = (ISelectionService)host.GetService(typeof(ISelectionService));
                if (service == null) {
                    return;
                }
                var components = new object[] { wizardControl };
                service.SetSelectedComponents(components, SelectionTypes.Replace);
                return;
            }
        }
    }
}