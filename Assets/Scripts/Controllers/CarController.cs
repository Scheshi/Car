using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Interfaces
{
    public class CarController : BaseController
    {
        private string _pathToPrefab = "Prefabs/car";
        private Car _model;
        private CarView _view;

        public CarController(Car model)
        {
            _model = model;
        }

        private CarView LoadView()
        {
            return Object.Instantiate(Resources.Load<CarView>(_pathToPrefab));
        }

        public void ChangeState(StateGame state)
        {
            switch (state)
            {
                case StateGame.Menu:
                    Dispose();
                    break;
                case StateGame.Game:
                    _view = LoadView();
                    AddGameObject(_view.gameObject);
                    break;
            }
        }
        
        
        
        
    }
}