using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingUI
{
    public class MainViewViewModel
    {
        #region Разовое открытие окна приложения
        //private ExternalCommandData _commandData;
        //public DelegateCommand SelectCommand { get; }

        //public MainViewViewModel(ExternalCommandData commandData)
        //{
        //    _commandData = commandData;
        //    SelectCommand = new DelegateCommand(OnSelectCommand);
        //}

        //public event EventHandler CloseRequest;
        //private void RaiseCloseRequest()
        //{
        //    CloseRequest?.Invoke(this, EventArgs.Empty);
        //}

        //private void OnSelectCommand()
        //{
        //    RaiseCloseRequest();

        //    UIApplication uiapp = _commandData.Application;
        //    UIDocument uidoc = uiapp.ActiveUIDocument;
        //    Document doc = uidoc.Document;

        //    var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
        //    var oElement = doc.GetElement(selectedObject);

        //    TaskDialog.Show("Сообщение", $"ID: {oElement.Id}");
        //}
        #endregion

        #region Многократное открытие окна приложения
        //private ExternalCommandData _commandData;
        //public DelegateCommand SelectCommand { get; }

        //public MainViewViewModel(ExternalCommandData commandData)
        //{
        //    _commandData = commandData;
        //    SelectCommand = new DelegateCommand(OnSelectCommand);
        //}

        //public event EventHandler HideRequest;
        //private void RaiseHideRequest()
        //{
        //    HideRequest?.Invoke(this, EventArgs.Empty);
        //}

        //public event EventHandler ShowRequest;
        //private void RaiseShowRequest()
        //{
        //    ShowRequest?.Invoke(this, EventArgs.Empty);
        //}

        //private void OnSelectCommand()
        //{
        //    RaiseHideRequest();


        //    UIApplication uiapp = _commandData.Application;
        //    UIDocument uidoc = uiapp.ActiveUIDocument;
        //    Document doc = uidoc.Document;

        //    var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
        //    var oElement = doc.GetElement(selectedObject);

        //    TaskDialog.Show("Сообщение", $"ID: {oElement.Id}");

        //    RaiseShowRequest();
        //}
        #endregion

        #region Многократное открытие окна приложения с добавлением метода выбора объекта в библиотеку SelectionUtils
        //private ExternalCommandData _commandData;
        //public DelegateCommand SelectCommand { get; }

        //public MainViewViewModel(ExternalCommandData commandData)
        //{
        //    _commandData = commandData;
        //    SelectCommand = new DelegateCommand(OnSelectCommand);
        //}

        //public event EventHandler HideRequest;
        //private void RaiseHideRequest()
        //{
        //    HideRequest?.Invoke(this, EventArgs.Empty);
        //}

        //public event EventHandler ShowRequest;
        //private void RaiseShowRequest()
        //{
        //    ShowRequest?.Invoke(this, EventArgs.Empty);
        //}

        //private void OnSelectCommand()
        //{
        //    RaiseHideRequest();

        //    Element oElement = SelectionUtils.PickObject(_commandData);
        //    TaskDialog.Show("Сообщение", $"ID: {oElement.Id}");

        //    RaiseShowRequest();
        //}
        #endregion

        #region Создание выпадающего списка
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }
        public List<Element> PickedObjects { get; } = new List<Element>();
        public List<PipingSystemType> PipeSystems { get; } = new List<PipingSystemType>();
        public PipingSystemType SelectedPipeSystem { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SaveCommand = new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickObjects(commandData);
            PipeSystems = PipesUtils.GetPipingSystems(commandData);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (PickedObjects.Count == 0 || SelectedPipeSystem == null)
                return;

            using (var ts = new Transaction(doc, "Set system type"))
            {
                ts.Start();
                foreach(var pickedObject in PickedObjects)
                {
                    if (pickedObject is Pipe)
                    {
                        var oPipe = pickedObject as Pipe;
                        oPipe.SetSystemType(SelectedPipeSystem.Id);
                    }
                }
                ts.Commit();
            }

            RaiseCloseRequest();
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }
}
