using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LevelUp.Permission
{
    public class Permission<T>
        where T : Enum
    {
        #region Private static fields

        private static Permission<T> _all;

        #endregion

        #region Private fields

        private T[] _policies;
        private BigInteger _value;

        #endregion

        #region Constructors

        public Permission() : this(0)
        {
        }

        public Permission(IEnumerable<T> policies) : this(ConvertToValue(policies))
        {
        }

        public Permission(params T[] policies) : this((IEnumerable<T>) policies)
        {
        }

        public Permission(int value) : this((BigInteger) value)
        {
        }

        public Permission(BigInteger value)
        {
            this.Value = value;
        }

        #endregion

        #region Public static properties

        public static Permission<T> All
        {
            get
            {
                return _all ??= new Permission<T>(ConvertToValue(Enum.GetValues(typeof(T)).OfType<T>().ToArray()))
                {
                    IsReadOnly = true
                };
            }
        }

        #endregion

        #region Public properties

        public T[] Policies
        {
            get { return _policies ??= Enum.GetValues(typeof(T)).OfType<T>().Where(item => Contains(item)).ToArray(); }
        }

        public BigInteger Value
        {
            get { return _value; }
            private set
            {
                if (this.IsReadOnly)
                    throw new ApplicationException("Readonly permission can't modify");
                _value = value;
                _policies = null;
            }
        }

        #endregion

        #region Private properties

        private bool IsReadOnly { get; set; }

        #endregion

        #region Public static methods

        public static Permission<T> operator +(Permission<T> permissionA, Permission<T> permissionB)
        {
            return permissionA.Value | permissionB.Value;
        }

        public static bool operator ==(Permission<T> permissionA, Permission<T> permissionB)
        {
            return permissionA.Equals(permissionB);
        }

        public static implicit operator BigInteger(Permission<T> permission)
        {
            return permission.Value;
        }

        public static implicit operator Permission<T>(BigInteger value)
        {
            return new Permission<T>(value);
        }

        public static implicit operator Permission<T>(int value)
        {
            return new Permission<T>(value);
        }

        public static bool operator !=(Permission<T> permissionA, Permission<T> permissionB)
        {
            return !permissionA.Equals(permissionB);
        }

        public static Permission<T> operator -(Permission<T> permissionA, Permission<T> permissionB)
        {
            return (permissionA.Value & All.Value) ^ permissionB.Value;
        }

        #endregion

        #region Public methods

        public Permission<T> Add(BigInteger value)
        {
            this.Value |= value;

            return this;
        }

        public Permission<T> Add(IEnumerable<T> policies)
        {
            return Add(ConvertToValue(policies));
        }

        public Permission<T> Add(params T[] policies)
        {
            return Add((IEnumerable<T>) policies);
        }

        public Permission<T> Add(Permission<T> permission)
        {
            return Add((this + permission).Value);
        }

        public bool Contains(BigInteger value)
        {
            return (this.Value & value) == value;
        }

        public bool Contains(params T[] policies)
        {
            return Contains((IEnumerable<T>) policies);
        }

        public bool Contains(IEnumerable<T> policies)
        {
            return Contains(ConvertToValue(policies));
        }

        public bool Contains(Permission<T> permission)
        {
            return Contains(permission.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;

            return this.Value == ((Permission<T>) obj).Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public Permission<T> Remove(BigInteger value)
        {
            this.Value &= All.Value ^ value;

            return this;
        }

        public Permission<T> Remove(IEnumerable<T> policies)
        {
            return Remove(ConvertToValue(policies));
        }

        public Permission<T> Remove(params T[] policies)
        {
            return Remove((IEnumerable<T>) policies);
        }

        public Permission<T> Remove(Permission<T> permission)
        {
            return Remove((this - permission).Value);
        }

        public override string ToString()
        {
            return string.Join(',', this.Policies);
        }

        #endregion

        #region Protected methods

        protected bool Equals(Permission<T> other)
        {
            return this.Value.Equals(other.Value);
        }

        #endregion

        #region Private static methods

        private static BigInteger ConvertToValue(params T[] policies)
        {
            return ConvertToValue((IEnumerable<T>) policies);
        }

        private static BigInteger ConvertToValue(IEnumerable<T> policies)
        {
            return policies.Aggregate((BigInteger) 0,
                (total, next) => total | (BigInteger) Math.Pow(2, (int) Enum.Parse(typeof(T), next.ToString())));
        }

        #endregion
    }
}