using System;
using System.Numerics;
using LevelUp.Permission.Tests.Enum;
using NUnit.Framework;

namespace LevelUp.Permission.Tests
{
    public class PermissionTests
    {
        [Test]
        public void Add_WithPolicy_PermissionWithPolicy()
        {
            // Arrange
            const Policy policy = Policy.Create;
            var target = new Permission<Policy>();
            var expected = (BigInteger) Math.Pow(2, (int) policy);
            var actual = default(BigInteger);

            // Act
            actual = target.Add(policy);
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Add_WithPermissionValue_PermissionWithPermissionValue()
        {
            // Arrange
            var target = new Permission<Policy>();
            var permissionValue =(BigInteger) Math.Pow(2, (int) Policy.Create);
            var expected = permissionValue;
            var actual = default(BigInteger);

            // Act
            actual = target.Add(permissionValue);
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void AddOperation_WithTwoPermission_PermissionWithTwoPermissions()
        {
            // Arrange
            var target1 = new Permission<Policy>(Policy.Create);
            var target2 = new Permission<Policy>(Policy.Read);
            var expected = new Permission<Policy>(Policy.Create, Policy.Read);
            var actual = default(Permission<Policy>);

            // Act
            actual = target1 + target2;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Remove_WithPolicy_PermissionWithoutPolicy()
        {
            // Arrange
            var target = new Permission<Policy>(Policy.Create, Policy.Read);
            var permissionValue =(BigInteger) Math.Pow(2, (int) Policy.Create);
            var expected = (BigInteger) Math.Pow(2, (int)  Policy.Read);
            var actual = default(BigInteger);

            // Act
            actual = target.Remove(permissionValue);
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Remove_WithPermissionValue_PermissionWithoutPermissionValue()
        {
            // Arrange
            const Policy policy = Policy.Create;
            var target = new Permission<Policy>(Policy.Create, Policy.Read);
            var expected = (BigInteger) Math.Pow(2, (int)  Policy.Read);
            var actual = default(BigInteger);

            // Act
            actual = target.Remove(policy);
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void RemoveOperation_WithTwoPermission_PermissionWithoutSpecifiedPermission()
        {
            // Arrange
            var target1 = new Permission<Policy>(Policy.Create, Policy.Read);
            var target2 = new Permission<Policy>(Policy.Read);
            var expected = new Permission<Policy>(Policy.Create);
            var actual = default(Permission<Policy>);

            // Act
            actual = target1 - target2;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void AllPermission_RemovePolicy_Exception()
        {
            // Arrange
            const Policy policy = Policy.Create;
            var target = Permission<Policy>.All;
            var actual = default(ApplicationException);

            // Act
            actual = Assert.Throws<ApplicationException>(()=>
            {
                target.Remove(policy);
            });
            
            // Assert
            Assert.IsNotNull(actual);
        }
    }
}