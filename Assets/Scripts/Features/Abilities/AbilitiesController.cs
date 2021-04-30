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
        public AbilitiesController(AbilityContainer container)
        {
            _model = new AbilitiesModel(container.Abilities);
        }

        public void Init(Transform uiPlace)
        {
            _view = LoadView<AbilitiesView>(_pathToPrefab, uiPlace);
            _view.Init(_model.Abilities, _model.ApplyAbility);
        }
    }
}