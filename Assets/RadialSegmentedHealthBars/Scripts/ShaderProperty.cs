using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RengeGames.HealthBars.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RengeGames.HealthBars {

	public interface IShaderProperty {
		void Reset();
		void ApplyToShader(bool ignoreDirty);
	}
	
	public class ShaderKeyword : IShaderProperty {
		[SerializeField]
		private bool _value;

		public bool Value {
			get { return _value; }
			set { 
				IsDirty = true;
				_value = value;
			}
		}

		public bool IsDirty { get; set; }
		private readonly string _keywordId;
		private Func<string, bool, bool, bool> _keywordFunc;

		public ShaderKeyword(string keywordId, Func<string, bool, bool, bool> keywordFunc, bool value = false) {
			_keywordId = keywordId;
			_keywordFunc = keywordFunc;
			_value = value;
			IsDirty = false;
		}

		public void Reset() {
			_value = _keywordFunc(_keywordId, false, false);
		}

		public void ApplyToShader(bool ignoreDirty) {
			if (IsDirty || ignoreDirty) {
				_keywordFunc(_keywordId, true, _value);
				IsDirty = false;
			}
		}

		public void ApplyValueToShader(bool ignoreDirty, ref bool value) {
			if (IsDirty && !ignoreDirty) {
				value = _value;
				_keywordFunc(_keywordId, true, _value);
				IsDirty = false;
			}else if (value != _value || ignoreDirty) {
				_value = value;
				_keywordFunc(_keywordId, true, _value);
				IsDirty = false;
			}
		}
	}
	
	public class ShaderProperty<T> : IShaderProperty {
		private T _value;

		public T Value {
			get { return _value; }
			set {
				IsDirty = true;
				_value = value;
			}
		}

		public bool IsDirty { get; set; }
		private readonly int _propertyId;
		private Func<int, bool, T, T> _propertyFunc;

		public ShaderProperty(string propertyName, Func<int, bool, T, T> propertyFunc, T value = default) {
			_propertyId = Shader.PropertyToID(propertyName);
			_propertyFunc = propertyFunc;
			//special case for gradients
			if (value is Gradient g) {
				_value = (T) (object) g.Clone();
			}
			else {
				_value = value;
			}
			IsDirty = false;
		}

		public void Reset() {
			_value = _propertyFunc(_propertyId, false, default);
			IsDirty = false;
		}

		public void ApplyToShader(bool ignoreDirty) {
			if (IsDirty || ignoreDirty) {
				_propertyFunc(_propertyId, true, _value);
				IsDirty = false;
			}
		}
		
		public void ApplyValueToShader(bool ignoreDirty, ref T value) {
			if (value == null) return;
			if (value is Gradient g) {
				ApplyGradientValueToShader(ignoreDirty, ref g);
				return;
			}
			if (IsDirty && !ignoreDirty) {
				value = _value;
				_propertyFunc(_propertyId, true, _value);
				IsDirty = false;
			}else if (ignoreDirty || !value.Equals(_value)) {
				_value = value;
				_propertyFunc(_propertyId, true, _value);
				IsDirty = false;
			}
		}

		private void ApplyGradientValueToShader(bool ignoreDirty, ref Gradient value) {
			if (value == null || !(_value is Gradient)) return;
			if (IsDirty && !ignoreDirty) {
				value = ((Gradient) (object) _value).Clone();
				_propertyFunc(_propertyId, true, _value);
				IsDirty = false;
			}else if (ignoreDirty || !value.EqualTo((Gradient) (object) _value)) {
				_value = (T) (object) value.Clone();
				_propertyFunc(_propertyId, true, _value);
				IsDirty = false;
			}
		}
	}
}