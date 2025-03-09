namespace Blazorit.Domain.Entities.Common;

public abstract class BaseIdEntity
{
    private long _id;

    public virtual long Id
    {
        get => _id;
        set => _id = value;
    }
    
    public static bool operator ==(BaseIdEntity left, BaseIdEntity right)
    {
        return object.Equals(left, right);
    }
    
    public static bool operator !=(BaseIdEntity left, BaseIdEntity right) => !(left == right);

    public override bool Equals(object? obj) => this.Equals(obj as BaseIdEntity);
    
    public override int GetHashCode()
    {
        return object.Equals((object)this.Id, (object)default(long)) ? base.GetHashCode() : this.Id.GetHashCode();
    }
    
    protected virtual bool Equals(BaseIdEntity? other)
    {
        if (other is null) return false;
        
        if ((object)this == (object)other)
        {
            return true;
        }

        if (BaseIdEntity.IsTransient(this)
            || BaseIdEntity.IsTransient(other)
            || !object.Equals((object)this.Id, (object)other.Id))
        {
            return false;
        }
        
        Type unproxiedType1 = this.GetUnproxiedType();
        Type unproxiedType2 = other.GetUnproxiedType();
        
        return unproxiedType1.IsAssignableFrom(unproxiedType2) || unproxiedType2.IsAssignableFrom(unproxiedType1);
    }

    private static bool IsTransient(BaseIdEntity obj) => object.Equals((object) obj.Id, (object) default(long));
    
    private Type GetUnproxiedType() => this.GetType();
}