﻿using System.Linq.Expressions;
using System.Reflection;
using WriteParameter.Exceptions;

namespace WriteParameter
{
    public class QueryGenerate<TEntity> : IQueryGenerate<TEntity>
        where TEntity : class
    {
        protected List<PropertyInfo> _properties;
        protected string _tableName;
        protected string _schema;
        protected PropertyInfo _idColumn;

        public QueryGenerate()
        {
            _properties = new List<PropertyInfo>();
        }
        public QueryGenerate(string tableName) : this()
        {
            _tableName = tableName;
        }
        public QueryGenerate(IEnumerable<PropertyInfo> properties)
        {
            _properties = properties.ToList();
        }
        public QueryGenerate(PropertyInfo[] properties)
        {
            _properties = properties.ToList();
        }


        public virtual string GenerateInsertQuery()
        {
            checkTable();
            checkSchema();
            return String.Format($"INSERT INTO {_schema}.{_tableName} {insertIntoWriteParameters()}").Replace("ı", "i");
        }

        public virtual string GenerateUpdateQuery()
        {
            checkTable();
            checkSchema();
            return String.Format($"update {_schema}.{_tableName} {updateWriteParameters()}").Replace("ı", "i");
        }
        public virtual string GenerateDeleteQuery()
        {
            checkTable();
            checkSchema();
            string idPropertyName = getIdColumn();
            return String.Format($"delete from {_schema}.{_tableName} where {idPropertyName}=@{idPropertyName}").Replace("ı", "i");
        }
        public virtual string GenerateGetAllQuery()
        {
            checkTable();
            checkSchema();
            string parameters = getParametersWithId("\"", "\"");
            string query = String.Format($"SELECT {parameters} FROM {_schema}.{_tableName}");
            return query.Replace("ı", "i");
        }

        public virtual IQueryGenerate<TEntity> SelectTable(string tableName)
        {
            _tableName = tableName.ToLower();
            return this;
        }
        public IQueryGenerate<TEntity> SelectTable(string tableName, string schema)
        {
            _tableName = tableName.ToLower();
            _schema = schema.ToLower();
            return this;
        }

        public IQueryGenerate<TEntity> SelectSchema(string schema)
        {
            _schema = schema.ToLower();
            return this;
        }

        public virtual string GenerateGetByIdQuery()
        {
            return generateGetByIdQuery();
        }
        public virtual string GenerateGetByIdQuery(int id)
        {
            return generateGetByIdQuery(id);
        }

        public virtual string GenerateGetByIdQuery(string id)
        {
            return generateGetByIdQuery(id);
        }

        protected virtual string generateGetByIdQuery(object id = null)
        {
            checkTable();
            checkSchema();
            string parameters = getParametersWithId();
            string idColumn = getIdColumn();
            string predicate = id == null ? $"{idColumn.ToLower()}=@{idColumn}" : $"{idColumn.ToLower()}={id}";
            string query = String.Format($"select {parameters} from {_schema}.{_tableName} where {predicate}");
            return query.Replace("ı", "i");
        }

        public virtual IQueryGenerate<TEntity> SelectColumn<TProperty>(Expression<Func<TEntity, TProperty>> predicate)
        {
            PropertyInfo propertyInfo = (predicate.Body as MemberExpression).Member as PropertyInfo;
            _properties.Add(propertyInfo);
            return this;
        }

        public virtual IQueryGenerate<TEntity> SelectIdColumn<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            PropertyInfo selectedIdColumn = (expression.Body as MemberExpression).Member as PropertyInfo;
            if (selectedIdColumn != null)
                _idColumn = selectedIdColumn;
            return this;
        }

        public IQueryGenerate<TEntity> SelectIdColumn<TProperty>(string idColumn)
        {
            PropertyInfo selectedIdColumn = typeof(TEntity).GetProperties().SingleOrDefault(x => x.Name.ToUpper() == idColumn.ToUpper());
            if (selectedIdColumn != null)
                _idColumn = selectedIdColumn;
            return this;
        }

        protected virtual void checkTable()
        {
            if (_tableName is null)
                throw new NoSelectedTableException();
        }
        protected virtual void checkSchema()
        {
            if (_schema is null)
                _schema = "dbo";//throw new NoSelectedSchemaException();
        }

        protected virtual void checkIdColumn()
        {
            if (_idColumn is null)
                throw new NotFoundIdColumnException();
        }

        protected virtual string getIdColumn()
        {
            if (_idColumn != null)
                return _idColumn.Name;

            var properties = typeof(TEntity).GetProperties().ToList();
            _idColumn = properties.FirstOrDefault(p => p.Name.ToUpper() == "ID");

            if (_idColumn is null)
            {
                _idColumn = properties.FirstOrDefault(p => p.Name.ToUpper().EndsWith("ID"));
                if (_idColumn is null)
                    _idColumn = properties.FirstOrDefault(p => p.Name.ToUpper().StartsWith("ID"));

                checkIdColumn();
            }
            return _idColumn.Name;
        }

        protected virtual string updateWriteParameters()
        {
            var properties = _properties.Count == 0 ? typeof(TEntity).GetProperties().ToList() : _properties;
            string idPropertyName = getIdColumn();

            string updateQuery = String.Join(",", properties.Select(p => p.Name == idPropertyName ? "" : $"{p.Name}=@{p.Name}"));

            updateQuery = updateQuery.StartsWith(",") ? updateQuery.Substring(1) : updateQuery;
            updateQuery += String.Concat(" ", $"WHERE {idPropertyName}=@{idPropertyName}");
            return $"set {updateQuery}";
        }
        protected virtual string insertIntoWriteParameters()
        {
            string columns = getParametersWithoutId();
            string valueColumns = getParametersWithoutId("@");
            return $"({columns}) VALUES ({valueColumns})";
        }

        protected virtual string getParametersWithoutId(string? previousName = "", string? lastName = "")
        {
            var properties = _properties.Count == 0 ? typeof(TEntity).GetProperties().ToList() : _properties;
            string idPropertyName = getIdColumn();
            string parameters = String.Join(",", properties.Select(p => p.Name == idPropertyName ? "" : $"{previousName}{p.Name}{lastName}"));
            parameters = parameters.StartsWith(",") ? parameters.Substring(1) : parameters;
            return parameters;
        }
        protected virtual string getParametersWithId(string? previousName = "", string? lastName = "")
        {
            var properties = _properties.Count == 0 ? typeof(TEntity).GetProperties().ToList() : _properties;
            string parameters = String.Join(",", properties.Select(p => $"{previousName}{p.Name}{lastName}"));
            parameters = parameters.StartsWith(",") ? parameters.Substring(1) : parameters;
            return parameters;
        }

    }
}
