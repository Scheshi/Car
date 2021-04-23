using Assets.Scripts.Configs;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Features.Abilities
{
    public class AbilitiesController : BaseController
    {
        private string _pathToPrefab = "Prefabs/Abilities";
        private AbilitiesModel _model;
        private AbilitiesView _view;
        public AbilitiesController(AbilityContainer container, Transform uiPlace)
        {
            _model = new AbilitiesModel(container.Abilities);
            _view = LoadView(uiPlace);
            AddGameObject(_view.gameObject);
            _view.gameObject.SetActive(false);
        }

        public void Init()
        {
            _view.gameObject.SetActive(true);
            _view.Init(_model.Abilities, _model.ApplyAbility);
        }

        private AbilitiesView LoadView(Transform uiTransform)
        {
            return Object.Instantiate(Resources.Load<AbilitiesView>(_pathToPrefab), uiTransform);
        }
    }
}